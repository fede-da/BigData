import subprocess

from flask import Flask, Blueprint, jsonify, request
from pymongo import MongoClient
import psycopg2
import mimetypes
import tempfile
import os
import time
import logging
from .cheshire_cat_config import CheshireCatConfig

app = Flask(__name__)
file_blueprint = Blueprint('file_blueprint', __name__)

# Configura il logging
logging.basicConfig(level=logging.INFO)
logger = logging.getLogger(__name__)

def forward_file(file):
    # Initialize the connection with Cheshire Cat
    config = CheshireCatConfig(base_url="localhost", port=1865, user_id="admin", auth_key="", secure_connection=False)
    cat_client = config.create_client()

    try:
        cat_client.connect_ws()
        # Wait until the WebSocket connection is established
        while not cat_client.is_ws_connected:
            time.sleep(1)

        # Determine the file extension
        _, file_extension = os.path.splitext(file['filename'])
        temp_dir = 'D:/Download/files'

        # Create a temporary file based on the file extension
        with tempfile.NamedTemporaryFile(delete=False, dir=temp_dir, suffix=file_extension) as temp_file:
            try:
                if file['content_type'].startswith('text') or file_extension == '.md' or file['content_type'] == 'application/json':
                    file_content = file['content']
                    temp_file.write(file_content.encode('utf-8'))
                else:
                    temp_file.write(file['content'])
                temp_file_path = temp_file.name
            except Exception as e:
                logger.error(f"Error writing temporary file: {str(e)}")
                return jsonify({'error': f'Error writing temporary file: {str(e)}'}), 500

        logger.info(f"Percorso del file temporaneo: {temp_file_path}")

        try:
            with open(temp_file_path, 'rb') as temp_file:
                response = cat_client.rabbit_hole.upload_file_without_preload_content(
                    file=temp_file.name,
                    chunk_size=None,
                    chunk_overlap=None
                )
                logger.info(f"Upload response: {response}")
        except Exception as e:
            logger.error(f"Failed to upload file to Cheshire Cat: {str(e)}")
            return jsonify({'error': f'Failed to upload file to Cheshire Cat: {str(e)}'}), 500
        finally:
            try:
                collections = cat_client.memory.get_collections()
                logger.info(f"Collections: {collections}")
            except Exception as e:
                logger.error(f"Failed to get collections from Cheshire Cat: {str(e)}")
                return jsonify({'error': f'Failed to get collections from Cheshire Cat: {str(e)}'}), 500
            cat_client.close()
            os.unlink(temp_file_path)

        return jsonify({'status': 'success', 'message': 'File forwarded successfully'}), 200

    except Exception as e:
        logger.error(f"Error connecting to Cheshire Cat: {str(e)}")
        return jsonify({'error': f'Error connecting to Cheshire Cat: {str(e)}'}), 500


@file_blueprint.route('/retrieve-and-forward-files/mongo', methods=['GET'])
def retrieve_and_forward_files_mongo():
    # Initialize MongoDB connection
    mongo_client = MongoClient("mongodb://admin:password@localhost:27017/")
    db = mongo_client['mydatabase']
    files_collection = db['dipendenti']

    # Retrieve files from the collection
    files = files_collection.find()

    # Loop through the files and forward each one
    for file_doc in files:
        file_content = (
            f"Nome: {file_doc['nome']}\n"
            f"Posizione: {file_doc['posizione']}\n"
            f"Dipartimento: {file_doc['dipartimento']}\n"
            f"Età: {file_doc['eta']}\n"
            f"Data Assunzione: {file_doc['data_assunzione']}\n"
            f"Email: {file_doc['email']}\n"
        )
        file = {
            'filename': f"{file_doc['nome'].replace(' ', '_')}.txt",
            'content_type': 'text/plain',
            'content': file_content
        }
        forward_file(file)

    return jsonify({'status': 'success', 'message': 'All files forwarded successfully'}), 200


@file_blueprint.route('/retrieve-and-forward-files/filesystem', methods=['GET'])
def retrieve_and_forward_files_filesystem():
    # Get the file path from the query parameters
    file_path = request.args.get('file_path')

    if not file_path:
        return jsonify({'error': 'No file path provided'}), 400

    # Check if file exists
    if not os.path.isfile(file_path):
        logger.error(f"File does not exist: {file_path}")
        return jsonify({'error': f"File does not exist: {file_path}"}), 404

    filename = os.path.basename(file_path)

    # Determine the MIME type based on the file extension
    mime_type, _ = mimetypes.guess_type(file_path)
    if mime_type is None:
        mime_type = 'application/octet-stream'  # Default MIME type for binary files

    try:
        # Read the file content
        with open(file_path, 'rb') as file:
            file_content = file.read()

        # If the file is a text file, ensure it's properly encoded
        if mime_type.startswith('text/') or mime_type in ['application/json', 'text/markdown']:
            file_content = file_content.decode('utf-8', errors='replace')  # Decode with error handling

        file_data = {
            'filename': filename,
            'content_type': mime_type,
            'content': file_content
        }

        return forward_file(file_data)

    except Exception as e:
        logger.error(f"Error reading file: {str(e)}")
        return jsonify({'error': f'Error reading file: {str(e)}'}), 500


@file_blueprint.route('/retrieve-and-forward-files/postgres', methods=['GET'])
def retrieve_and_forward_files_postgres():
    try:
        # Initialize PostgreSQL connection
        conn = psycopg2.connect(
            dbname="postgres",
            user="postgres",
            password="postgres",
            host="localhost",
            port="5432"
        )
        cursor = conn.cursor()

        # Esegui la query e ottieni il risultato
        sql_query = "SELECT nome, posizione, dipartimento, eta, data_assunzione, email FROM dipendenti"
        result = subprocess.run([
            'docker', 'exec', 'postgres-rag-app',
            'psql', '-U', 'postgres', '-d', 'postgres', '-c', sql_query
        ], capture_output=True, text=True)

        # Analizza il risultato della query
        output_lines = result.stdout.splitlines()

        # Salta le righe di intestazione e quelle vuote
        data = []
        for line in output_lines:
            # Rimuovi eventuali righe vuote e intestazioni
            line = line.strip()
            if line and '|' in line:
                parts = line.split('|')
                if len(parts) == 6:  # Assicurati che ci siano esattamente 6 colonne
                    nome = parts[0].strip()
                    posizione = parts[1].strip()
                    dipartimento = parts[2].strip()
                    try:
                        eta = int(parts[3].strip())
                    except ValueError:
                        eta = None  # Gestisci valori non numerici per l'età
                    data_assunzione = parts[4].strip()
                    email = parts[5].strip()
                    data.append((nome, posizione, dipartimento, eta, data_assunzione, email))

                    file_content = (
                        f"Nome: {nome}\n"
                        f"Posizione: {posizione}\n"
                        f"Dipartimento: {dipartimento}\n"
                        f"Età: {eta}\n"
                        f"Data Assunzione: {data_assunzione}\n"
                        f"Email: {email}\n"
                    )

                    # Crea il dizionario del file
                    file = {
                        'filename': f"{nome.replace(' ', '_')}.txt",
                        'content_type': 'text/plain',
                        'content': file_content
                    }

                    forward_file(file)

        cursor.close()
        conn.close()

        return jsonify({'status': 'success', 'message': 'All files forwarded successfully'}), 200

    except psycopg2.Error as e:
        logger.error(f"Error connecting to PostgreSQL: {str(e)}")
        return jsonify({'error': f'Error connecting to PostgreSQL: {str(e)}'}), 500

    except Exception as e:
        logger.error(f"Unexpected error: {str(e)}")
        return jsonify({'error': f'Unexpected error: {str(e)}'}), 500

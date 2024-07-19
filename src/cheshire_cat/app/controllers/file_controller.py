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
        with open(file_path, 'rb') as file:  # Open in binary mode
            file_content = file.read()
    except Exception as e:
        logger.error(f"Error reading file: {str(e)}")
        return jsonify({'error': f'Error reading file: {str(e)}'}), 500

    file_data = {
        'filename': filename,
        'content_type': mime_type,
        'content': file_content
    }

    return forward_file(file_data)


@file_blueprint.route('/retrieve-and-forward-files/postgres', methods=['GET'])
def retrieve_and_forward_files_postgres():
    # Initialize PostgreSQL connection
    try:
        conn = psycopg2.connect(
            dbname="postgres",
            user="postgres",
            password="postgres",
            host="localhost",
            port="5432"
        )
        cursor = conn.cursor()

        cursor.execute("SELECT nome, posizione, dipartimento, eta, data_assunzione, email FROM dipendenti")
        rows = cursor.fetchall()

        # Loop through the rows and forward each one
        for row in rows:
            file_content = (
                f"Nome: {row[0]}\n"
                f"Posizione: {row[1]}\n"
                f"Dipartimento: {row[2]}\n"
                f"Età: {row[3]}\n"
                f"Data Assunzione: {row[4]}\n"
                f"Email: {row[5]}\n"
            )
            file = {
                'filename': f"{row[0].replace(' ', '_')}.txt",
                'content_type': 'text/plain',
                'content': file_content
            }
            forward_file(file)

        cursor.close()
        conn.close()

        return jsonify({'status': 'success', 'message': 'All files forwarded successfully'}), 200

    except Exception as e:
        logger.error(f"Error connecting to PostgreSQL: {str(e)}")
        return jsonify({'error': f'Error connecting to PostgreSQL: {str(e)}'}), 500
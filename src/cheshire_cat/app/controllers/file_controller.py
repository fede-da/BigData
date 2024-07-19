import os
import tempfile
import json
import time
from flask import Flask, Blueprint, request, jsonify, Response
from pymongo import MongoClient
from bson import ObjectId
import cheshire_cat_api as ccat
from .cheshire_cat_config import CheshireCatConfig

file_blueprint = Blueprint('file', __name__)

# MongoDB connection details
mongo_client = MongoClient("mongodb://admin:password@localhost:27017/")
db = mongo_client['mydatabase']
files_collection = db['dipendenti']


@file_blueprint.route('/retrieve-file/<string:file_id>', methods=['GET'])
def retrieve_file(file_id):
    try:
        # Find the document in MongoDB by its ID
        file_document = files_collection.find_one({"_id": ObjectId(file_id)})

        if file_document is None:
            return jsonify({'error': 'Document not found'}), 404

        # Convert the document to JSON format and save it as a temporary file
        file_content = json.dumps(file_document, default=str)  # Serialize document to JSON
        file_name = f"{file_document.get('nome', 'employee')}.json"

        # Create a temporary file
        _, file_extension = os.path.splitext(file_name)
        temp_dir = 'D:/Download/files'

        with tempfile.NamedTemporaryFile(delete=False, dir=temp_dir, suffix=file_extension) as temp_file:
            temp_file.write(file_content.encode('utf-8'))  # Write JSON data to the file
            temp_file_path = temp_file.name

        # Return the file path in JSON format
        return jsonify({'status': 'success', 'file_path': temp_file_path}), 200

    except Exception as e:
        return jsonify({'error': f'Error retrieving file from MongoDB: {str(e)}'}), 500


@file_blueprint.route('/forward-file/<string:file_id>', methods=['POST'])
def forward_file(file_id):
    try:
        # Retrieve the file from MongoDB
        retrieve_response = retrieve_file(file_id)

        # Ensure the retrieve_response is an instance of Flask's Response
        if isinstance(retrieve_response, Response) and retrieve_response.status_code == 200:
            response_json = retrieve_response.get_json()
            file_path = response_json.get('file_path')

            # Initialize the connection with Cheshire Cat
            config = CheshireCatConfig(base_url="localhost", port=1865, user_id="admin", auth_key="",
                                       secure_connection=False)
            cat_client = config.create_client()
            cat_client.connect_ws()

            # Wait until the WebSocket connection is established
            while not cat_client.is_ws_connected:
                time.sleep(1)

            try:
                with open(file_path, 'rb') as temp_file:
                    file_extension = os.path.splitext(file_path)[1].lower()

                    if file_extension == '.json':
                        file_content = temp_file.read()
                        memories = json.loads(file_content.decode('utf-8'))

                        # Call the ingest_memory method with the JSON file content
                        cat_client.rabbit_hole.ingest_memory(cat_client, memories)
                    else:
                        # Send the temporary file to Cheshire Cat
                        upload_response = cat_client.rabbit_hole.upload_file_without_preload_content(
                            file=temp_file,  # Pass the file object directly
                            chunk_size=None,
                            chunk_overlap=None,
                        )
            except Exception as e:
                return jsonify({'error': f'Failed to upload file to Cheshire Cat: {str(e)}'}), 500
            finally:
                # Close the WebSocket connection
                cat_client.close()
                # Remove the temporary file
                os.unlink(file_path)

            return jsonify({'status': 'success', 'message': 'File forwarded successfully'}), 200

        # If retrieve_response is not a successful Response
        return retrieve_response

    except Exception as e:
        return jsonify({'error': f'Failed to forward file: {str(e)}'}), 500
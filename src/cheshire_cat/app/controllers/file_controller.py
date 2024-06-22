import os
import tempfile
import time
from flask import Flask, Blueprint, request, jsonify
import cheshire_cat_api as ccat
from .cheshire_cat_config import CheshireCatConfig

file_blueprint = Blueprint('file', __name__)

@file_blueprint.route('/forward-file', methods=['POST'])
def forward_file():
    # Check if the 'file' field is present in the request
    if 'file' not in request.files:
        return jsonify({'error': 'No file part'}), 400

    file = request.files['file']

    # Check that the file name is not empty
    if file.filename == '':
        return jsonify({'error': 'No selected file'}), 400

    # Initialize the connection with Cheshire Cat
    config = CheshireCatConfig(base_url="localhost", port=1865, user_id="admin", auth_key="", secure_connection=False)
    cat_client = config.create_client()
    cat_client.connect_ws()

    # Wait until the WebSocket connection is established
    while not cat_client.is_ws_connected:
        time.sleep(1)

    # Determine the file extension
    _, file_extension = os.path.splitext(file.filename)

    # Create a temporary file based on the file extension
    with tempfile.NamedTemporaryFile(delete=False, suffix=file_extension) as temp_file:
        try:
            if file.content_type.startswith('text'):
                file_content = file.stream.read().decode('utf-8')
                temp_file.write(file_content.encode('utf-8'))
            else:
                # Otherwise, save the file directly
                file.save(temp_file)

            temp_file_path = temp_file.name

        except Exception as e:
            return jsonify({'error': f'Error writing temporary file: {str(e)}'}), 500

    print("Percorso del file temporaneo:", temp_file_path)

    try:
        with open(temp_file_path, 'rb') as temp_file:
            # Send the temporary file to Cheshire Cat
            response = cat_client.rabbit_hole.upload_file_without_preload_content(
                file=temp_file.name,
                chunk_size=None,
                chunk_overlap=None,
                #_request_timeout=(20, 20)
            )
    except Exception as e:
        return jsonify({'error': f'Failed to upload file to Cheshire Cat: {str(e)}'}), 500
    finally:
        # Close the WebSocket connection
        cat_client.close()
        # Remove the temporary file
        os.unlink(temp_file_path)

    return jsonify({'status': 'success', 'message': 'File forwarded successfully'}), 200

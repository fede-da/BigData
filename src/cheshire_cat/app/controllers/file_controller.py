from flask import Blueprint, request, jsonify, current_app
import requests

file_blueprint = Blueprint('file', __name__)


@file_blueprint.route('/forward-file', methods=['POST'])
def forward_file():
    if 'file' not in request.files:
        return jsonify({'error': 'No file part'}), 400

    file = request.files['file']
    if file.filename == '':
        return jsonify({'error': 'No selected file'}), 400

    if file:
        url = current_app.config['FORWARD_URL']

        # Prepare the file in the appropriate format for sending
        files = {'file': (file.filename, file.stream, file.content_type)}

        # Send the file using POST request
        response = requests.post(url, files=files)

        # Handle the response from the external server
        if response.status_code == 200:
            return jsonify({'status': 'success', 'message': 'File forwarded successfully'}), 200
        else:
            return jsonify({'status': 'error', 'message': 'Failed to forward file'}), 500

    return jsonify({'error': 'File processing error'}), 500

from flask import Flask, Blueprint, request, jsonify, current_app
import requests
import cheshire_cat_api as ccat
import CheshireCatConfig 
import time 

file_blueprint = Blueprint('file', __name__)


@file_blueprint.route('/forward-file', methods=['POST'])
def forward_file():
    if 'file' not in request.files:
        return jsonify({'error': 'No file part'}), 400

    file = request.files['file']
    if file.filename == '':
        return jsonify({'error': 'No selected file'}), 400 

    if file:
        # url = current_app.config['FORWARD_URL']

        # # Prepare the file in the appropriate format for sending
        # files = {'file': (file.filename, file.stream, file.content_type)}

        # # Send the file using POST request
        # response = requests.post(url, files=files)

        # # Handle the response from the external server
        # if response.status_code != 200:
        #      return jsonify({'error': 'Failed to forward file to external server'}), 500
        # # else:
        # #     return jsonify({'status': 'success', 'message': 'File forwarded successfully'}), 200
        

        config = CheshireCatConfig.CheshireCatConfig(base_url="localhost", port=1865, user_id="user", auth_key="", secure_connection=False)
        cat_client = config.create_client()

        cat_client.connect_ws()

        while not cat_client.is_ws_connected: 
            time.sleep(1)

        cat_client.send(message="Hello Cat!")

        time.sleep(1)

        if cat_client.is_ws_connected:
            # Leggi il contenuto del file come bytes
            file_content_bytes = file.read()

            # Converti il contenuto in stringa, se necessario
            file_content_string = file_content_bytes.decode('utf-8')  # Supponendo che il file sia in formato utf-8

            print("File content as bytes:", file_content_bytes)
            print("File content as string:", file_content_string)
            print("Type of file content:", type(file_content_bytes))

            try:
                response = cat_client.rabbit_hole.upload_file(
                    file=file_content_string,  
                    chunk_size=1000,  
                    chunk_overlap=100,  
                    _request_timeout=(20, 20)  
                )
            except Exception as e:
                # Gestione degli errori durante il caricamento del file
                return jsonify({'error': f'Failed to upload file to Cheshire Cat: {str(e)}'}), 500
            finally:
                cat_client.close()
            return jsonify({'status': 'success', 'message': 'File forwarded successfully'}), 200
        else:
            return jsonify({'error': 'Failed to establish WebSocket connection with Cheshire Cat'}), 500

    # Delete the episodic and declarative memories
    #response = cat_client.memory.wipe_collections()

    return jsonify({'error': 'File processing error'}), 500

# Crea un'applicazione Flask
app = Flask(__name__)

# Registra il blueprint nell'applicazione Flask
app.register_blueprint(file_blueprint)

# Avvia il server Flask
if __name__ == '__main__':
    app.run(debug=True)
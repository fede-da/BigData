# Description
This project provides an API endpoint for forwarding files to Cheshire Cat. 

## Configuration
You need to configure the base URL, port, user ID, and optional authentication key to connect to Cheshire Cat. This can be done using the `CheshireCatConfig` class defined in the `cheshire_cat_config.py` module.

## Usage
The API offers a `forward-file` endpoint that accepts POST requests containing files to be forwarded to Cheshire Cat. API endpoints are implemented using Flask.

## Implementation
The code uses the `cheshire_cat_api` library to communicate with the Cheshire Cat service. Files are forwarded via a WebSocket connection.

## Additional Notes
Currently, the system only supports forwarding PDF files. 
While the code is designed to handle all types of text files (`text/plain`), it currently lacks proper handling for file types such as Markdown, HTML, and others. If you want to forward files of these types, you need to make modifications to the implementation to ensure proper handling.

import requests

from constants.api import api
from constants.question import question_input


def get_question():
    return question_input


def create_request_for_hugging_face_api(question):
    headers = {"Authorization": api}
    API_URL = "https://api-inference.huggingface.co/models/ItalianMLDevs/TestForCheshire"
    payload = {"inputs": question}
    return {"url": API_URL, "headers": headers, "json": payload}


def send_http_request_to_hugging_face(request):
    response = requests.post(request['url'], headers=request['headers'], json=request['json'])
    return response


# Example usage
question = get_question()
request = create_request_for_hugging_face_api(question)
response = send_http_request_to_hugging_face(request)
print(response.json())

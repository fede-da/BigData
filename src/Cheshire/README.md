# Project for Big Data

## Prerequisites

Install the following:
* transformers
* huggingface-cli

## Setting up

Clone this repository and in [constants](./constants) folder create a new file called "api.py". Then, past there the following line:

`api = "Bearer [YOUR HUGGING_FACE API KEY]"`

Replace the placeholder with your Hugging Face API key

## Asking for completions

Run the script `send_question.py` to request a completion, to send a different question edit `question_input` variable into `question.py` file.

## Errors 

Sometimes Hugging Face returns the "file not ready" error, try running the `send_question.py` multiple times
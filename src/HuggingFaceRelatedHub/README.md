# Intro

Hugging Face Hub hosts the [model](https://huggingface.co/ItalianMLDevs/TestForCheshire) used for inference from Cheshire Cat.
This document explains how to handle this section

## Prerequisites

Install the following:

* huggingface-cli

## Setting up

Clone this repository and in [constants](./constants) folder create a new file called "api.py". Then, past there the following line:

`api = "Bearer [YOUR HUGGING_FACE API KEY]"`

Replace the placeholder with your Hugging Face API key.

Run the script [set_up_env.sh](/src/HuggingFaceRelatedHub/scripts/set_up_env.sh) to prepare the environment.

## Asking for completions

Run the script [send_request.sh](/src/HuggingFaceRelatedHub/scripts/send_request.sh) to request a completion, to send a different question edit `question_input` variable into `question.py` file.

## Errors 

Sometimes Hugging Face returns the "file not ready" error, try running the [send_request](/src/HuggingFaceRelatedHub/scripts/send_request.sh) script multiple times
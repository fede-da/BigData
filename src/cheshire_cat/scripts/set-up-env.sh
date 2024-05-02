#!/bin/bash

# Name of the variable
var_name="CHESHIRE_CLIENT_PROJECT_PATH"

# Check if the environment variable is set
if [ -z "${!var_name}" ]; then
    echo "Error: $var_name is not set."
    exit 1
else
    # Change the directory to the one specified by the environment variable
    cd "${!var_name}" || { echo "Failed to change directory to ${!var_name}"; exit 1; }
    echo "Successfully changed directory to ${!var_name}"
    python3 -m venv venv
    echo "Successfully created venv environment"
fi
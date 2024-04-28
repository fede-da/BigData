# Intro

This document guides the developer through the process of correctly setting up Cheshire Cat framework.
The official guide is available at the following [link](https://github.com/cheshire-cat-ai/core)

## Set up

Run the following command to run Cheshire Docker container:

`docker run --rm -it -p 1865:80 ghcr.io/cheshire-cat-ai/core:latest`

## Interact with it

Chat with the Cheshire Cat on `localhost:1865/admin`.
You can also interact via REST API and try out the endpoints on `localhost:1865/docs`.

## Scripts

Run cat using `start-cat.sh`

Stop cat using `stop-cat.sh`
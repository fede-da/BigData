# this entire class must be double checked, where does the JSON file comes from?


import json

from injector import singleton


@singleton
class ConfigService:
    def __init__(self, filename):
        with open(filename, 'r') as f:
            self.config = json.load(f)

    def get_rabbitmq_hostname(self):
        return self.config['rabbitmq']['hostname']

    def get_input_queue_name(self):
        return self.config['rabbitmq']['input_queue']

    def get_output_queue_name(self):
        return self.config['rabbitmq']['output_queue']

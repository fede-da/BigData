# pip install datasets <- required, it uses hugging face dataset library

from back_end.ai.constants import *
from datasets import Dataset


class DatasetHandler:
    my_dataset: Dataset

    def __init__(self):
        _questions, _answers = [], []
        _file = open(OUTPUT_QUESTIONS_FILE, "r")
        _questions = _file.readlines()
        _file.close()
        _file = open(OUTPUT_ANSWERS_FILE, "r")
        _answers = _file.readlines()
        _file.close()
        tmp_dataset = {'questions': _questions, 'answers': _answers}
        self.my_dataset = Dataset.from_dict(tmp_dataset)


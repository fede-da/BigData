from transformers import GPT2Tokenizer, GPT2LMHeadModel
import torch
from back_end.ai.constants import OUTPUT_MODEL_FOLDER


class AiService:
    _tokenizer = GPT2Tokenizer.from_pretrained(OUTPUT_MODEL_FOLDER)
    #_model = GPT2LMHeadModel.from_pretrained(OUTPUT_MODEL_FOLDER)
    _model = GPT2LMHeadModel.from_pretrained(OUTPUT_MODEL_FOLDER, from_tf=True)


    def __init__(self):
        # Check for available device and move the model to it
        self.device = torch.device("cuda" if torch.cuda.is_available() else "cpu")
        self._model.to(self.device)
        self._model.eval()

    def generate_response(self, question, max_length=150):
        # Encode the question and retrieve the initial tensor (context)
        input_ids = self._tokenizer.encode(question, return_tensors='pt').to(self.device)

        # Generate a response using the model
        with torch.no_grad():
            response_ids = self._model.generate(input_ids, max_length=max_length, num_return_sequences=1, pad_token_id=self._tokenizer.eos_token_id)

        # Decode and return the response
        response = self._tokenizer.decode(response_ids[0], skip_special_tokens=True)
        return response








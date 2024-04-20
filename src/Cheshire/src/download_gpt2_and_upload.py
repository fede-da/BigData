from transformers import GPT2LMHeadModel, GPT2Tokenizer

model = GPT2LMHeadModel.from_pretrained('gpt2')
tokenizer = GPT2Tokenizer.from_pretrained('gpt2')

model_name = "ItalianMLDevs/TestForCheshire"
model.push_to_hub(model_name)
tokenizer.push_to_hub(model_name)


from transformers import LlamaForCausalLM, LlamaTokenizer

# Load LLaMA 3 model and tokenizer
model = LlamaForCausalLM.from_pretrained('meta-llama/Llama-2-7b-hf')
tokenizer = LlamaTokenizer.from_pretrained('meta-llama/Llama-2-7b-hf')

# Define the model name on Hugging Face Hub
hub_name = "ItalianMLDevs/TestForCheshire"

# Push the model and tokenizer to the Hugging Face Hub
model.push_to_hub(hub_name)
tokenizer.push_to_hub(hub_name)
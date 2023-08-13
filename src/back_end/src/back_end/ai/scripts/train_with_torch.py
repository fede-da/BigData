import torch
from torch.utils.data import DataLoader, TensorDataset
from transformers import GPT2Tokenizer, GPT2LMHeadModel, AdamW
from back_end.ai.constants import *
from back_end.ai.utils.DatasetHandler import DatasetHandler

# Checking available GPU
device = torch.device("cuda" if torch.cuda.is_available() else "cpu")
print("Using:", device)

# Read processed data
dataset_handler = DatasetHandler()
lines = dataset_handler.my_dataset['questions'] + dataset_handler.my_dataset['answers']

# Load the tokenizer
tokenizer = GPT2Tokenizer.from_pretrained("gpt2-medium")
# EOS -> End of sequence
tokenizer.pad_token = tokenizer.eos_token

# Encoding
input_ids = [tokenizer.encode(text, max_length=MAX_LENGTH, truncation=True, padding='max_length') for text in lines]

# tags token as 1 or 0
attention_masks = [[1 if token_id > 0 else 0 for token_id in input_id] for input_id in input_ids]

# Creates 2 tensor: tokens and attention mask
input_ids = torch.tensor(input_ids, dtype=torch.long)
attention_masks = torch.tensor(attention_masks, dtype=torch.long)

# Creates the dataset, "input_is" is both input and target here
dataset = TensorDataset(input_ids, attention_masks, input_ids)
dataloader = DataLoader(dataset, shuffle=True, batch_size=BATCH_SIZE)

# Load the pre-trained GPT-2 model
model = GPT2LMHeadModel.from_pretrained("gpt2-medium")
model.to(device)

# Define custom loss, Cross Entry Loss is just one of the most commonly used
loss_function = torch.nn.CrossEntropyLoss()

# Optimizes some workload
optimizer = AdamW(model.parameters(), lr=LEARNING_RATE)

# Training loop
model.train()

for epoch in range(EPOCHS):
    total_loss = 0
    for idx, (input_ids_batch, attention_mask_batch, labels_batch) in enumerate(dataloader):
        input_ids_batch, attention_mask_batch, labels_batch = input_ids_batch.to(device), attention_mask_batch.to(
            device), labels_batch.to(device)

        # Sets gradient to 0
        optimizer.zero_grad()

        # Data is passed to the model to get the output
        outputs = model(input_ids_batch, attention_mask=attention_mask_batch, labels=labels_batch)
        loss = outputs.loss
        # Calculate gradients of the loss
        loss.backward()

        # Updates the model
        optimizer.step()

        total_loss += loss.item()

        # Every 10 batches print status
        if idx % 10 == 0:
            print(f"Epoch: {epoch}, Batch: {idx}, Loss: {loss.item() / len(input_ids_batch)}")

# Save the model and tokenizer after training
model.save_pretrained(OUTPUT_MODEL_FOLDER)
tokenizer.save_pretrained(OUTPUT_MODEL_FOLDER)

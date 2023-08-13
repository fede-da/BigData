import torch
from torch.utils.data import Dataset, DataLoader, TensorDataset, random_split
from transformers import GPT2Tokenizer, GPT2LMHeadModel, AdamW

from back_end.ai.constants import *
from back_end.ai.utils.DatasetHandler import DatasetHandler

# Checking available GPU
device = torch.device("cuda" if torch.cuda.is_available() else "cpu")
print("Using:", device)


# Load the tokenizer
tokenizer = GPT2Tokenizer.from_pretrained("gpt2-medium")
tokenizer.pad_token = tokenizer.eos_token

# Read preprocessed data
dataset_handler = DatasetHandler()
lines = dataset_handler.my_dataset['questions'] + dataset_handler.my_dataset['answers']


input_ids = [tokenizer.encode(text, max_length=MAX_LENGTH, truncation=True, padding='max_length') for text in lines]
attention_masks = [[1 if token_id > 0 else 0 for token_id in input_id] for input_id in input_ids]

input_ids = torch.tensor(input_ids, dtype=torch.long)
attention_masks = torch.tensor(attention_masks, dtype=torch.long)

dataset = TensorDataset(input_ids, attention_masks, input_ids)
dataloader = DataLoader(dataset, shuffle=True, batch_size=BATCH_SIZE)

# Load the pre-trained GPT-2 model
model = GPT2LMHeadModel.from_pretrained("gpt2-medium")
model.to(device)

# Define custom loss
loss_function = torch.nn.CrossEntropyLoss()

# Optimizer
optimizer = AdamW(model.parameters(), lr=LEARNING_RATE)

# Training loop
model.train()

for epoch in range(EPOCHS):
    total_loss = 0
    for idx, (input_ids_batch, attention_mask_batch, labels_batch) in enumerate(dataloader):
        input_ids_batch, attention_mask_batch, labels_batch = input_ids_batch.to(device), attention_mask_batch.to(
            device), labels_batch.to(device)
        #if idx >= 9:
        #    break
        optimizer.zero_grad()

        outputs = model(input_ids_batch, attention_mask=attention_mask_batch, labels=labels_batch)
        loss = outputs.loss
        loss.backward()
        optimizer.step()

        total_loss += loss.item()
        if idx % 10 == 0:
            print(f"Epoch: {epoch}, Batch: {idx}, Loss: {loss.item() / len(input_ids_batch)}")

# Save the model and tokenizer after training
model.save_pretrained(OUTPUT_MODEL_FOLDER)
tokenizer.save_pretrained(OUTPUT_MODEL_FOLDER)

'''
import tensorflow as tf
from transformers import GPT2Tokenizer, TFGPT2LMHeadModel

from back_end.ai.utils.DatasetHandler import DatasetHandler

# Checking available GPU
print("Num GPUs Available: ", len(tf.config.experimental.list_physical_devices('GPU')))


# Parameters
BATCH_SIZE = 4
EPOCHS = 1
MAX_LENGTH = 400
LEARNING_RATE = 3e-4

# Load the tokenizer
tokenizer = GPT2Tokenizer.from_pretrained("gpt2-medium")
tokenizer.pad_token = tokenizer.eos_token

# Prepare dataset
custom_dataset = DatasetHandler()

input_ids = [tokenizer.encode(text, max_length=MAX_LENGTH, truncation=True, padding='max_length') for text in lines]
attention_masks = [[1 if token_id > 0 else 0 for token_id in input_id] for input_id in input_ids]

input_ids = tf.convert_to_tensor(input_ids, dtype=tf.int32)
attention_masks = tf.convert_to_tensor(attention_masks, dtype=tf.int32)

dataset = tf.data.Dataset.from_tensor_slices((input_ids, attention_masks, input_ids))
dataset = dataset.shuffle(buffer_size=len(input_ids)).batch(BATCH_SIZE)

# Load the pre-trained GPT-2 model
model = TFGPT2LMHeadModel.from_pretrained("gpt2-medium")

# Define custom loss
loss_object = tf.keras.losses.SparseCategoricalCrossentropy(from_logits=True)

# Optimize for Mac
optimizer = tf.keras.optimizers.legacy.Adam(learning_rate=LEARNING_RATE)


def compute_loss(labels, logits):
    mask = tf.math.logical_not(tf.math.equal(labels, 0))
    loss_ = loss_object(labels, logits)
    mask = tf.cast(mask, dtype=loss_.dtype)
    loss_ *= mask
    return tf.reduce_sum(loss_)/tf.reduce_sum(mask)


@tf.function
def train_step(input_ids, attention_mask, labels):
    with tf.GradientTape() as tape:
        logits = model(input_ids, attention_mask=attention_mask).logits
        loss = compute_loss(labels, logits)
    gradients = tape.gradient(loss, model.trainable_variables)
    optimizer.apply_gradients(zip(gradients, model.trainable_variables))
    return loss


# Training loop
for epoch in range(EPOCHS):
    total_loss = 0
    for idx, (input_ids_batch, attention_mask_batch, labels_batch) in enumerate(dataset):
        batch_loss = train_step(input_ids_batch, attention_mask_batch, labels_batch)
        total_loss += batch_loss
        if idx % 10 == 0:
            print(f"Epoch: {epoch}, Batch: {idx}, Loss: {batch_loss.numpy() / len(input_ids_batch)}")

# Save the model and tokenizer after training
model.save_pretrained("models/finetuned")
tokenizer.save_pretrained("models/finetuned")
'''
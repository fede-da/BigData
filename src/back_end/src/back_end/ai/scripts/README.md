# Overview

This section contains a brief description about `preprocess.py` script and a longer one about `train_with_tensorflow.py`.

The first script loads all the raw data needed for a new training cycle, makes some operations like: "cleaning, validation"...And creates new files that contain processed data ready to be consumed by the model.

The 2nd one is responsible for all the training process that can be broken down to the following steps:

- Tokenizer loading and tokenization

- Attention masks definition

- PyTorch tensor

### Tokenizer loading and tokenization

In `Natural Language Processing`, Models can't be directly trained on words because they need of numerical input so `tokens` are used instead; they are just words broken down to smaller pieces, the entire process is called `Tokenization`.

So, the `Tokenizer` is the object that has the responsibility of loading `processed text data` breaking them down to tokens through tokenization process.

When this process is completed then a dictionary can be built enumerating all generated tokens in unique `Key:Value` pairs as shown in the example below that it's an extract from `vocab.json` file:

```
{
... some values
"document": 22897,
  "documented": 47045,
  "does": 22437,
  "doesn": 45084,
... more values
}
```

Tokenizer is directly loaded from Gpt2 medium model, the following line of code:

tokenizer.pad_token = tokenizer.eos_token

sets `pad token` (its goal is to create standard sentences of the same length) to `end of file token`, this is required otherwise an error occurs.

### Attention masks definition

An `attention mask` defines a sequence of `1` and `0` that marks processable and not processable tokens; as mentioned above, models require sentences of the same length so sometimes extra padding is added to standardize inputs. 

In summary, it's used to help model to distinguish useful and useless tokens.

### PyTorch tensor 

Tensors are arrays of different dimensions that are usually processed by neural networks; examples are neural networks parameters that are stored as tensors.

One of their most important features is the ability to be transferred to a GPU; this can process many tensors in parallel leading to faster trainings.











































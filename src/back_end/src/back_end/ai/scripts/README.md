# Script

## Overview

This section contains a brief description about `preprocess.py` script and a longer one about `train_with_tensorflow.py`.

The first one loads all the raw data needed for a new training cycle, makes some operations like: "cleaning, validation"...And creates new files that contain processed data ready to be consumed by the model.

The 2nd one is responsible for all the training process that can be broken down to the following steps:

- Processed data loading
- 
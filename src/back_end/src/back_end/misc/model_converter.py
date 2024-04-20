import os

import tensorflow as tf

# Get the current working directory
current_directory = os.getcwd()

print("Current Directory:", current_directory)


model = tf.keras.models.load_model('../ai/models/finetuned/tf_model.h5')
tf.saved_model.save(model, 'new_tf_model')

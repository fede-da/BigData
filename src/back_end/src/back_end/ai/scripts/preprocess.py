from back_end.ai.constants import *


# Read the original data
_input_file = open(INPUT_FILE, "r")
lines = _input_file.readlines()

# Separate inputs and targets
questions, answers, rejected = [], [], []
for line in lines:
    processed_line = line.strip().split('\t')
    if len(processed_line) != 2:
        rejected.append(line)
    else:
        questions_text, answers_text = processed_line
        questions.append(questions_text)
        answers.append(answers_text)


# Writes results to file

def save_data(output_path, _data):
    _file = open(output_path, "w")
    for s in _data:
        _file.write(s + "\n")
    _file.close()


save_data(OUTPUT_QUESTIONS_FILE, questions)
save_data(OUTPUT_ANSWERS_FILE, answers)
save_data(OUTPUT_REJECTED_FILE, rejected)


print("Preprocessing completed!")



'''
# Load the tokenizer
tokenizer = GPT2Tokenizer.from_pretrained("gpt2-medium")
tokenizer.pad_token = tokenizer.eos_token

# Read the original data
with open(INPUT_FILE, "r") as file:
    lines = file.readlines()

# Separate inputs and targets
questions, answers, rejected = [], [], []
for line in lines:
    processed_line = line.strip().split('\t')
    if len(processed_line) != 2:
        rejected.append(line)
    else:
        questions_text, answers_text = processed_line
        questions.append(questions_text)
        answers.append(answers_text)


def tokenize_batch(_questions, _answers, _tokenizer: PreTrainedTokenizerFast):
    _tokenized_questions = tokenizer.batch_encode_plus(
        _questions,
        truncation=True,
        padding='longest',  # Pad to the longest sequence in the batch
        return_tensors='tf'
    )
    _tokenized_answers = tokenizer.batch_encode_plus(
        _answers,
        truncation=True,
        padding='longest',
        return_tensors='tf'
    )
    return _tokenized_questions, _tokenized_answers


# Call tokenize function assigning first and second result, should be input_ids on train script
tokenized_questions, tokenized_answers = tokenize_batch(questions, answers, tokenizer)


# Writes results to file
def save_tokenized_data(_tokenized_questions, _tokenized_answers, questions_path, answers_path):
    qf = open(questions_path, 'wb')
    pickle.dump(_tokenized_questions, qf)
    qf.close()

    af = open(answers_path, 'wb')
    pickle.dump(_tokenized_answers, af)
    af.close()


save_tokenized_data(tokenized_questions, tokenized_answers, OUTPUT_QUESTIONS_FILE, OUTPUT_ANSWERS_FILE)

print("Preprocessing completed!")
'''



































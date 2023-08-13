from pathlib import Path


# Paths
PROJECT_ROOT = Path(__file__).parent
INPUT_FILE = PROJECT_ROOT / "data/raw/casual_talk.txt"
OUTPUT_QUESTIONS_FILE = PROJECT_ROOT / "data/processed/questions.txt"
OUTPUT_ANSWERS_FILE = PROJECT_ROOT / "data/processed/answers.txt"
OUTPUT_REJECTED_FILE = PROJECT_ROOT / "data/processed/rejected.txt"
OUTPUT_LOG_FOLDER = PROJECT_ROOT / "data/logs/"
OUTPUT_RESULT_FOLDER = PROJECT_ROOT / "data/results/"
OUTPUT_MODEL_FOLDER = PROJECT_ROOT / "models/finetuned/"


# Values
MAX_LENGTH = 400
BATCH_SIZE = 4
EPOCHS = 1
LEARNING_RATE = 3e-4


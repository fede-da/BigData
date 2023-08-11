
from injector import singleton

from src.back_end.ai.ai_main import AiMain
from src.back_end.ai.services.AiServiceInterface import AiServiceInterface


@singleton
class AiService(AiServiceInterface):
    ai: AiMain

    def __init__(self):
        pass

    def process_message(self, msg: str):
        AiMain()
        return ""
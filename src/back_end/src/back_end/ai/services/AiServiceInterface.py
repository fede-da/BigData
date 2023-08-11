from abc import abstractmethod


class AiServiceInterface:
    @abstractmethod
    def process_message(self, msg: str):
        pass


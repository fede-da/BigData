import json
from dataclasses import dataclass


@dataclass
class MessageDto:
    guid: str
    message: str

    @classmethod
    def from_json(cls, json_str: str):
        data = json.loads(json_str)
        # Adjusting the keys from the JSON to match the expected attribute names
        adjusted_data = {
            "guid": data.get("Guid", None),
            "message": data.get("Message", None)
        }
        return cls(**adjusted_data)

    def print_members(self):
        for attr, value in self.__dict__.items():
            print(f'{attr}: {value}')

    def to_json(self):
        return json.dumps({
            "guid": self.guid,
            "message": self.message
        })

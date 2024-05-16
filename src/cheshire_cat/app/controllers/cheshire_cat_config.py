import cheshire_cat_api as ccat

class CheshireCatConfig:
    def __init__(self, base_url="localhost", port=1865, user_id="user", auth_key="", secure_connection=False):
        self.base_url = base_url
        self.port = port
        self.user_id = user_id
        self.auth_key = auth_key
        self.secure_connection = secure_connection

    def create_client(self, on_open=None, on_close=None, on_message=None, on_error=None):
        config = ccat.Config(
            base_url=self.base_url,
            port=self.port,
            user_id=self.user_id,
            auth_key=self.auth_key,
            secure_connection=self.secure_connection
        )
        cat_client = ccat.CatClient(
            config=config,
            on_open=on_open,
            on_close=on_close,
            on_message=on_message,
            on_error=on_error
        )
        return cat_client

from flask import Flask
from config import config_by_name

def create_app(config_name='dev'):
    app = Flask(__name__)
    app.config.from_object(config_by_name[config_name])

    from .controllers.file_controller import file_blueprint
    app.register_blueprint(file_blueprint, url_prefix='/api')

    return app

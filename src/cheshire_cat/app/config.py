import os


class Config:
    """Base configuration."""
    SECRET_KEY = os.environ.get('SECRET_KEY', 'your_secret_key_here')
    SQLALCHEMY_DATABASE_URI = os.environ.get('DATABASE_URL', 'sqlite:///yourdatabase.db')
    SQLALCHEMY_TRACK_MODIFICATIONS = False
    FORWARD_URL = 'https://example.com/receive-file'  # Default forward URL


class DevelopmentConfig(Config):
    """Development configuration."""
    DEBUG = True
    SQLALCHEMY_ECHO = True


class TestingConfig(Config):
    """Testing configuration, with separate database."""
    TESTING = True
    SQLALCHEMY_DATABASE_URI = 'sqlite:///test.db'


class ProductionConfig(Config):
    """Production configuration."""
    DEBUG = False
    FORWARD_URL = os.environ.get('FORWARD_URL', 'https://production.example.com/receive-file')


config_by_name = dict(
    dev=DevelopmentConfig,
    test=TestingConfig,
    prod=ProductionConfig
)

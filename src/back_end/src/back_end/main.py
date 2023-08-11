from injector import Injector

from src.back_end.di.container import AppModule
from src.back_end.services.config_service import ConfigService
from src.back_end.services.rabbit_mq_service import RabbitMqService


def main():
    injector = Injector(AppModule())

    config_service = injector.get(ConfigService)
    rabbitmq_service = injector.get(RabbitMqService)

main()


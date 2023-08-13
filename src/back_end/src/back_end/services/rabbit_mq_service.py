import pika
from injector import inject, singleton
from back_end.ai.ai_service import AiService
from back_end.dtos.MessageDto import MessageDto
from back_end.services.config_service import ConfigService


@singleton
class RabbitMqService:

    @inject
    def __init__(self, config_service: ConfigService, ai_service:AiService):
        self.config_service = config_service
        self.ai_service = ai_service
        self.connection = pika.BlockingConnection(pika.ConnectionParameters(self.config_service.get_rabbitmq_hostname()))
        self.channel = self.connection.channel()
        self.start_consuming(self._on_message_callback)

    def start_consuming(self, callback):
        queue_name = self.config_service.get_input_queue_name()
        self.channel.queue_declare(queue=queue_name,durable=False)
        self.channel.basic_consume(queue=queue_name, on_message_callback=callback, auto_ack=True)

        print(f' [*] Waiting for messages in {queue_name}. To exit press CTRL+C')
        self.channel.start_consuming()

    def send_to_rabbitmq(self, message_dto: MessageDto):
        name = self.config_service.get_output_queue_name()
        # Setup connection and channel

        # Declare the queue (make sure it exists)
        self.channel.queue_declare(queue=name, durable=False)
        message_dto.message = "Python response: " + message_dto.message
        # Send the message
        self.channel.basic_publish(
            exchange='',
            routing_key=name,
            body=message_dto.to_json(),
            properties=pika.BasicProperties(
                delivery_mode=2,  # Make message persistent
            ))

        print(f" [x] Sent {message_dto.to_json()}")

    def _on_message_callback(self, ch, method, properties, body):
        print("Received message: ", body)
        msg: MessageDto = MessageDto.from_json(body)
        msg.print_members()
        # send data to ai
        response = self.ai_service.generate_response(msg.message)
        print("IA response is: " + response)
        # wait for response
        # update MessageDto with ai response
        msg.message = response
        # send data
        self.send_to_rabbitmq(msg)




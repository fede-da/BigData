
using FrontEndServer.Client.ChatBot_UI.Models.Messages;
using FrontEndServer.Server.Hubs.ChatbotMessageHub;
using FrontEndServer.Shared.Dtos;
using Microsoft.AspNetCore.SignalR;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

//TODO: Mica che posso sempre cambia coda ogni post e get...Come si fa ad impostare il servizio?

namespace FrontEndServer.Server.Data.Service.RabbitMq
{
    public class RabbitMqService : IRabbitMqService
    {
        private readonly IModel _channel;
        private readonly IConnection _connection;  
        private readonly IRabbitMqConfiguration _configuration;
        private readonly string _queueToListen = "python";
        private readonly string _queueToSend = "dotnet";
        private bool _disposed = false;
        private readonly IHubContext<ChatbotMessageHub> _hubContext;
        private readonly ILogger<RabbitMqService> _logger;
        private readonly JsonSerializerOptions _jsonSerializerOptions;
        // TODO: Define an event that will be triggered when a message is received

        public RabbitMqService(IRabbitMqConfiguration configuration, IHubContext<ChatbotMessageHub> hubContext, ILogger<RabbitMqService> logger)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            var factory = new ConnectionFactory() { HostName = _configuration.HostName };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _logger = logger;
            _hubContext = hubContext;
            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            // Action to be performed when a message is received, public event maybe later
            // MessageReceived += OnMessageReceived;


        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            // Logic to be executed on application startup
            DeclareQueues();
            StartListening();
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            // Cleanup logic or anything you'd like to run when the application is shutting down
            _channel.Close();
            _connection.Close();
        }

        private void DeclareQueues()
        {
            foreach (var queue in _configuration.RabbitQueues)
            {
                _channel.QueueDeclare(
                queue: queue.Name,
                durable: queue.Durable,
                exclusive: queue.Exclusive,
                autoDelete: queue.AutoDelete);

                //_channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);

            }
            return;
        }

        // RabbitMQ is a message broker: it accepts and forwards messages.

        // This service listens to queue "python" and sends messages to queue "dotnet", these queues are defined in appsettings.json
        // TODO: When a message is received nothing happens, modify the OnMessageReceived method to handle the MessageDTO
        public void StartListening()
        {
            var consumer = new EventingBasicConsumer(_channel);
            _logger.LogInformation($"Start listening on queue {_queueToListen}");
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var data = Encoding.UTF8.GetString(body.ToArray());

                // Deserialize the received data into the MessageDTO object
                try
                {
                    _logger.LogInformation($"Received new message from Rabbit {data}");
                    var receivedMessageDTO = JsonSerializer.Deserialize<MessageDTO>(data,_jsonSerializerOptions);
                    _logger.LogInformation($"Deserialized message: {receivedMessageDTO}");
                    OnMessageReceived(receivedMessageDTO!);
                }
                catch (Exception e)
                {
                    _logger.LogError($"Error while deserializing the received message from Rabbit: {e.Message}");
                }
            };

            _channel.BasicConsume(queue: _queueToListen, autoAck: true, consumer: consumer);
        }


        // Modify the OnMessageReceived method to handle the MessageDTO
        private void OnMessageReceived(MessageDTO receivedMessageDTO)
        {
            try
            {
                // TODO: Send data to the UI with SignalR
                var groupName = $"AnonymousUser-{receivedMessageDTO.Guid}";
                _hubContext.Clients.Group(groupName).SendAsync("ReceiveMessage", receivedMessageDTO.Message);
                _logger.LogInformation($"Sent message {receivedMessageDTO.Message} to AnonymousUser-{receivedMessageDTO.Guid}");
            } catch (Exception e)
            {
                _logger.LogError($"Error while sending the message to the UI: {e.Message}");
            }
        }


        public void SendMessage(MessageDTO message)
        {

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

            _channel.BasicPublish(exchange: "",
                routingKey: _queueToSend,
                basicProperties: null,
                body: body);

            Console.WriteLine($"Sent message: {message}");
        }

        public void Dispose()
        {
            if (_disposed) return;

            _channel?.Close();
            _connection?.Close();

            _disposed = true;
            GC.SuppressFinalize(this);
        }

        ~RabbitMqService()
        {
            Dispose();
        }
    }

}

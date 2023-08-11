
using FrontEndServer.Shared.Dtos;

namespace FrontEndServer.Server.Data.Service.RabbitMq
{
    public interface IRabbitMqService : IDisposable, IHostedService
    {
        void SendMessage(MessageDTO message);
        void StartListening();
    }
}

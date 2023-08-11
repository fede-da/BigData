using FrontEndServer.Server.Config;

namespace FrontEndServer.Server.Data.Service.RabbitMq
{
    public interface IRabbitMqConfiguration
    {
        string HostName { get; }
        IEnumerable<RabbitQueue> RabbitQueues { get; }
    }

}

using FrontEndServer.Server.Config;
using Microsoft.Extensions.Options;

namespace FrontEndServer.Server.Data.Service.RabbitMq
{
    public class RabbitMqConfiguration : IRabbitMqConfiguration
    {
        private readonly IOptions<AppSettings> _appSettings;

        public RabbitMqConfiguration(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings ?? throw new ArgumentNullException(nameof(appSettings));
        }

        public string HostName => _appSettings.Value.RabbitMQ.HostName;

        public IEnumerable<RabbitQueue> RabbitQueues => _appSettings.Value.RabbitMQ.RabbitQueues;
    }

}

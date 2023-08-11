namespace FrontEndServer.Server.Config
{
    // This cnfiguration is used to read configurations from appsettings.json
    public class AppSettings
    {
        public RabbitMq RabbitMQ { get; set; }

        public ConnectionStrings ConnectionStrings { get; set; }
    }

    public class ConnectionStrings
    {
        public string DefaultConnection { get; set; }
    }

    public class RabbitMq
    {
        public string HostName { get; set; }
        public IEnumerable<RabbitQueue> RabbitQueues { get; set; }

        public RabbitQueue? GetQueue(string queueName) => RabbitQueues.FirstOrDefault(q => q.Name == queueName);
    }

    public class RabbitQueue
    {
        public string Name { get; set; }
        public bool Durable { get; set; }
        public bool Exclusive { get; set; }
        public bool AutoDelete { get; set; }
    }

}

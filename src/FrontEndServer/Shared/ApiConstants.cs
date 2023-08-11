
namespace FrontEndServer.Shared
{
    public static class ApiConstants
    {
        public static class RabbitTest
        {
            public const string HostName = "localhost"; // This should ideally be in a config or environment variable
            public const string QueueName = "test";
            public const string BaseUrl = "api/rabbit/test";
        }

        public static class RabbitMqControllerConstants
        {
            public const string BaseUrl = "api/rabbit";
            public const string Send = "send";

            // Using a property instead of a method for path combining
            public static string SendPath => $"{BaseUrl}/{Send}";
        }

        public static class HubConstants
        {
            public const string BaseUrl = "api/hub";
            public const string ChatbotMessageHub = "chatbotmessagehub";
        }
    }

}

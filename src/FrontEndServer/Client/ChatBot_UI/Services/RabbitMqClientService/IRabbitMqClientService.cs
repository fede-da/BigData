namespace FrontEndServer.Client.ChatBot_UI.Services.RabbitMqClientService
{
    public interface IRabbitMqClientService
    {
        Task SendMessageAsync(string message);
    }
}

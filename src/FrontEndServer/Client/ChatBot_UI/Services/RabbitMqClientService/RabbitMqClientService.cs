using FrontEndServer.Client.ChatBot_UI.Services.RabbitMqClientService;
using FrontEndServer.Shared;
using FrontEndServer.Shared.Dtos;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FrontEndServer.Client.ChatBot_UI.Services.RabbitMqClientService
{
    public class RabbitMqClientService : IRabbitMqClientService
    {
        private readonly HttpClient _httpClient;

        public RabbitMqClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task SendMessageAsync(string message)
        {
            var dto = new MessageDTO { Message = message };
            var messageContent = new StringContent(
                JsonSerializer.Serialize(dto),
                Encoding.UTF8,
                "application/json"
            );

            Console.WriteLine($"Sending message: {message} on path {ApiConstants.RabbitMqControllerConstants.SendPath}");
            var response = await _httpClient.PostAsync($"{ApiConstants.RabbitMqControllerConstants.SendPath}", messageContent);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Failed to send message. Status code: {response.StatusCode}");
            }
        }


    }

}

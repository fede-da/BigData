using FrontEndServer.Server.Data.Services.GuidGenerator;
using Microsoft.AspNetCore.SignalR;

namespace FrontEndServer.Server.Hubs.ChatbotMessageHub
{
    public class ChatbotMessageHub : Hub
    {
        private readonly ILogger<ChatbotMessageHub> _logger;
        private readonly IGuidGenerator _guidGenerator;
        public ChatbotMessageHub(ILogger<ChatbotMessageHub> logger, IGuidGenerator generator) {
            _logger = logger;
            _guidGenerator = generator;
        }
        public override async Task OnConnectedAsync()
        {
            try
            {
                var guid =_guidGenerator.GetGeneratedGuid();
                if (guid != null)
                {
                    var groupName = $"AnonymousUser-{guid}";
                    await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
                }

                _logger.LogInformation($"User connected with connectionId: {Context.ConnectionId} and group name: AnonymousUser-{guid}");

                await base.OnConnectedAsync();
            } catch (Exception ex)
            {
                  Console.WriteLine(ex.Message);
            }
        }

    }
}

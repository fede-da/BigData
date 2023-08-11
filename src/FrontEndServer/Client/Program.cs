
using FrontEndServer.Client.ChatBot_UI.Services.RabbitMqClientService;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace FrontEndServer.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            // Register the AppSettings object
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            // Register custom services
            builder.Services.AddScoped<IRabbitMqClientService, RabbitMqClientService>();

            await builder.Build().RunAsync();
        }
    }
}
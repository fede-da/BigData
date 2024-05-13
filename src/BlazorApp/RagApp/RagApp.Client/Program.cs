using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using RagApp.Client;
using RagApp.Services.CheshireCatService;

namespace RagApp.Client
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            //builder.Services.AddAuthorizationCore();
            //builder.Services.AddCascadingAuthenticationState();

            builder.Services.AddHttpClient<ICheshireCatService, CheshireCatService>();
            //builder.Services.AddSingleton<AuthenticationStateProvider, PersistentAuthenticationStateProvider>();

            await builder.Build().RunAsync();
        }
    }
}

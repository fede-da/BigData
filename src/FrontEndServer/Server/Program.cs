using FrontEndServer.Server.Config;
using FrontEndServer.Server.Data.Service.RabbitMq;
using FrontEndServer.Server.Data.Services.GuidGenerator;
using FrontEndServer.Server.Hubs.ChatbotMessageHub;
using FrontEndServer.Shared;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace FrontEndServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            ConfigureServices(builder);

            var app = builder.Build();

            // Trigger validation for AppSettings
            var settings = app.Services.GetService<AppSettings>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();
            app.UseRouting();

            app.MapRazorPages();
            app.MapControllers();
            app.MapFallbackToFile("index.html");
            app.MapHub<ChatbotMessageHub>(ApiConstants.HubConstants.BaseUrl);
            app.Run();
        }

        static void ConfigureServices(WebApplicationBuilder builder)
        {
            builder.Services.AddLogging(builder => builder
            .AddConsole());
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            // Register the AppSettings object
            builder.Services.AddSingleton<IValidateOptions<AppSettings>, AppSettingsValidator>();
            builder.Services.Configure<AppSettings>(builder.Configuration);

            // Register AppSettings for triggering validation
            builder.Services.AddTransient(provider =>
            {
                var options = provider.GetRequiredService<IOptions<AppSettings>>();
                return options.Value;
            });

            builder.Services.AddSignalR();

            // configuring custom services
            builder.Services.AddSingleton<IRabbitMqConfiguration, RabbitMqConfiguration>();
            builder.Services.AddSingleton<IRabbitMqService, RabbitMqService>();
            builder.Services.AddSingleton<IGuidGenerator, GuidGenerator>();
            builder.Services.AddHostedService<RabbitMqService>();
        }
    }
}
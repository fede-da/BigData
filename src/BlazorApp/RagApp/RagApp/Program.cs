using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RagApp.Client.Pages;
using RagApp.Components;
using RagApp.DAL;
using RagApp.Services.CheshireCatService;

namespace RagApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            Startup.ConfigureServices(builder.Services);

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<PostgresDbContext>(options =>
                //options.UseSqlServer(connectionString)
                options.UseNpgsql(connectionString)
                );

            builder.Services.AddScoped<ICheshireCatService, CheshireCatService>();
            builder.Services.AddHttpClient();

            var app = builder.Build();

            Startup.Configure(app, app.Environment);

            app.Run();
        }
    }
}

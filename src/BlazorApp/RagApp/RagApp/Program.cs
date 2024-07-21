using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RagApp.Client.Pages;
using RagApp.Components;
using RagApp.DAL;
using RagApp.DAL.MongoModels;
using RagApp.Services.CheshireCatService;
using RagApp.Services.PostgresService;

namespace RagApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configura i servizi
            Startup.ConfigureServices(builder.Services);

            var postgresConnectionString = builder.Configuration.GetConnectionString("DefaultPostgreConnection") ?? throw new InvalidOperationException("Connection string 'DefaultPostgreConnection' not found.");

            builder.Services.Configure<CheshireCatDatabaseSettings>(
                builder.Configuration.GetSection("MongoCheshireCatDb"));

            // Configura il contesto di database PostgreSQL
            builder.Services.AddDbContext<PostgresDbContext>(options =>
                options.UseNpgsql(postgresConnectionString)
            );

            builder.Services.AddScoped<CsvService>();

            builder.Services.AddHttpClient();

            var app = builder.Build();

            // Configura l'app
            Startup.Configure(app, app.Environment);

            app.Run();
        }
    }
}

using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RagApp.Client.Pages;
using RagApp.Components;
using RagApp.DAL;
using RagApp.DAL.MongoModels;
using RagApp.Services.CheshireCatService;

namespace RagApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            Startup.ConfigureServices(builder.Services);

            var postgresConnectionString = builder.Configuration.GetConnectionString("DefaultPostgreConnection") ?? throw new InvalidOperationException("Connection string 'DefaultPostgreConnection' not found.");

            builder.Services.Configure<CheshireCatDatabaseSettings>(
                builder.Configuration.GetSection("MongoCheshireCatDb"));

            builder.Services.AddDbContext<PostgresDbContext>(options =>
                //options.UseSqlServer(connectionString)
                options.UseNpgsql(postgresConnectionString)
                );



            builder.Services.AddHttpClient();
            builder.Services.AddScoped<ICheshireCatService, CheshireCatService>();
            
            var app = builder.Build();

            Startup.Configure(app, app.Environment);

            app.Run();
        }
    }
}

using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RagApp.Client.Pages;
using RagApp.Components;
using RagApp.DAL;

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

            //builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

            var app = builder.Build();

            Startup.Configure(app, app.Environment);

            app.Run();
        }
    }
}

﻿using Microsoft.AspNetCore.Identity;
using RagApp.Components;
using RagApp.DAL;
using RagApp.DAL.Repositories.Mongo.MongoEmployeeService;
using RagApp.DAL.Repositories.Postgres.PostgresDipendenteService;
using RagApp.Services;
using RagApp.Services.CheshireCatService;

namespace RagApp
{
    public class Startup
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorComponents()
                .AddInteractiveServerComponents()
                .AddInteractiveWebAssemblyComponents();

            services.AddCascadingAuthenticationState();

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            })
                .AddIdentityCookies();

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<PostgresDbContext>()
                .AddSignInManager()
                .AddDefaultTokenProviders();

            services.AddScoped<IFileService, FileService>();
            services.AddScoped<ICheshireCatService, CheshireCatService>();
            services.AddScoped<IMongoEmployeeService, MongoEmployeeService>();
            services.AddScoped<IPostgresDipendenteService, PostgresDipendenteService>(); // Aggiungi questa riga
            services.AddSingleton<MongoDbContext>();
            services.AddControllers();
        }

        public static void Configure(WebApplication app, IWebHostEnvironment env)
        {
            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                app.UseWebAssemblyDebugging();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode()
                .AddInteractiveWebAssemblyRenderMode()
                .AddAdditionalAssemblies(typeof(Client._Imports).Assembly);

            // Add additional endpoints required by the Identity /Account Razor components.
            //app.MapAdditionalIdentityEndpoints();
        }
    }
}

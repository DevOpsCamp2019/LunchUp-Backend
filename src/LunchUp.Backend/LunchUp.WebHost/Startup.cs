using System.IO;
using AutoMapper;
using LunchUp.Core.Integration;
using LunchUp.Core.Matching;
using LunchUp.Model.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Npgsql;


#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
namespace LunchUp.WebHost
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));
            services.AddMvcCore().AddApiExplorer();

            var connection = Configuration.GetSection("Database:lunchup").Get<DatabaseSettings>();
            var connectionString = new NpgsqlConnectionStringBuilder
            {
                Database = connection.Database, Host = connection.Host, Port = connection.Port,
                Username = connection.Username, Password = connection.Password
            };

            services.AddEntityFrameworkNpgsql().AddDbContext<LunchUpContext>(opt =>
                opt.UseNpgsql(connectionString.ConnectionString));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "LunchUp API",
                    Version = "V1",
                    Description = "The Documentation for the LunchUp API",
                    Contact = new OpenApiContact
                    {
                        Name = "CU DevOps",
                        Email = "354ccef8.Zuhlke.onmicrosoft.com@emea.teams.ms"
                    }
                });

                var xmlFile = Path.ChangeExtension(typeof(Startup).Assembly.Location, ".xml");
                c.IncludeXmlComments(xmlFile);
            });

            services.AddTransient<IMatchingService, SimpleMatchingService>();
            services.AddTransient<IIntegrationService, IntegrationService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            UpdateDatabase(app);
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();
            app.UseHsts();
            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "LunchUp API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseRouting();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });


        }

        private static void UpdateDatabase(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope();
            using var context = serviceScope.ServiceProvider.GetService<LunchUpContext>();
            context.Database.Migrate();
        }
    }
}
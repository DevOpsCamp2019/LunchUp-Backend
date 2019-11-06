using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LunchUp.Core.Common;
using LunchUp.Core.Integration;
using LunchUp.Core.Matching;
using LunchUp.Model;
using LunchUp.WebHost.Extension;
using LunchUp.WebHost.HealthCheck;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
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

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(jwtOptions =>
                {
                    jwtOptions.Authority =
                        $"https://{Configuration["AzureAdB2C:Hostname"]}/tfp/{Configuration["AzureAdB2C:Tenant"]}/{Configuration["AzureAdB2C:Policy"]}/v2.0/";
                    jwtOptions.Audience = Configuration["AzureAdB2C:ClientId"];
                    jwtOptions.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = AuthenticationFailed
                    };
                    jwtOptions.RequireHttpsMetadata = false;
                    jwtOptions.SaveToken = true;
                    jwtOptions.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true
                    };
                });
            services.AddAuthorization();

            var connection = Configuration.GetSection("Database:lunchup").Get<DatabaseSettings>();
            var connectionString = new NpgsqlConnectionStringBuilder
            {
                Database = connection.Database, Host = connection.Host, Port = connection.Port,
                Username = connection.Username, Password = connection.Password
            };

            services.AddMvcCore().AddApiExplorer().ConfigureApiBehaviorOptions(
                options => { options.SuppressMapClientErrors = true; });

            services.AddDbContext<LunchUpContext>(opt =>
                opt.UseNpgsql(connectionString.ConnectionString)
            );

            services.Configure<KestrelServerOptions>(options => { options.AllowSynchronousIO = true; });

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

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description =
                        "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });

                var xmlFile = Path.ChangeExtension(typeof(Startup).Assembly.Location, ".xml");
                c.IncludeXmlComments(xmlFile);
            });

            services.AddTransient<ICommonService, CommonService>();
            services.AddTransient<IMatchingService, SimpleMatchingService>();
            services.AddTransient<IIntegrationService, IntegrationService>();

            services.AddHealthChecks()
                .AddCheck("LunchDbHealthCheck", new NpgSqlConnectionHealthCheck(connectionString.ConnectionString));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                IdentityModelEventSource.ShowPII = true;
            }

            app.MigrateDatabase<LunchUpContext>();
            app.UseHsts();
            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "LunchUp API V1");
                c.RoutePrefix = string.Empty;
            });


            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            app.UseHealthChecks("/api/health");
        }

        private Task AuthenticationFailed(AuthenticationFailedContext arg)
        {
            // For debugging purposes only!
            var s = $"AuthenticationFailed: {arg.Exception.Message}";
            arg.Response.ContentLength = s.Length;
            arg.Response.Body.Write(Encoding.UTF8.GetBytes(s), 0, s.Length);
            return Task.FromResult(0);
        }
    }
}
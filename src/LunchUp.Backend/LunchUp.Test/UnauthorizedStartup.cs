using LunchUp.Model;
using LunchUp.WebHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.Testing;

namespace LunchUp.Test
{
    public class UnauthorizedStartup : Startup
    {
        public UnauthorizedStartup(IConfiguration configuration): base(configuration) { }        

        protected override void ConfigureDatabase(IServiceCollection services)
        {
            services.AddDbContext<LunchUpContext>(
                options => options.UseInMemoryDatabase(databaseName: "LunchUpTestDb")
            );
        }
    }
}

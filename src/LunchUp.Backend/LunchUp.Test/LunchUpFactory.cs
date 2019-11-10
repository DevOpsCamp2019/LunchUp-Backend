using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace LunchUp.Test
{
    public class LunchUpFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder(null)
                .ConfigureWebHostDefaults(webbuilder =>
            {
                webbuilder.UseStartup<TStartup>();
            });
        }

    }
}

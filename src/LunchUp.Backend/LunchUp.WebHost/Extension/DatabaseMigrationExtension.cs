using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LunchUp.WebHost.Extension
{
    /// <summary>
    ///     Extension for IApplicationBuilder
    /// </summary>
    public static class DatabaseMigrationExtension
    {
        /// <summary>
        ///     Migrates the Database
        /// </summary>
        /// <param name="app">The IApplicationBuilder</param>
        public static void MigrateDatabase<T>(this IApplicationBuilder app) where T : DbContext
        {
            using var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope();
            using var context = serviceScope.ServiceProvider.GetService<T>();
            context.Database.Migrate();
        }
    }
}
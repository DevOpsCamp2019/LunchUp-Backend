using System;
using LunchUp.Model;
using LunchUp.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace LunchUp.Test.Core
{
    public class ServiceTestBase : IDisposable
    {
        protected readonly LunchUpContext LunchUpContext;
        protected readonly PersonEntity Entity = new PersonEntity()
        {
            Id = new Guid("3aeba5b5-7e9e-4786-8df2-c191565f161e"),
            Firstname = "John", 
            Lastname = "Doe", 
            Email = "john.doe@anonymous.com"
        };

        public ServiceTestBase()
        {
            var builder = new DbContextOptionsBuilder<LunchUpContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            DbContextOptions<LunchUpContext> options = builder.Options;

            LunchUpContext = new LunchUpContext(options);
            LunchUpContext.Database.EnsureDeleted();
            LunchUpContext.Database.EnsureCreated();
        }

        public void Dispose()
        {
            LunchUpContext?.Dispose();
        }
    }
}

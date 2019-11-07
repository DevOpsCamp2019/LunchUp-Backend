using System;
using LunchUp.Model;
using LunchUp.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace LunchUp.Test.Core
{
    public class ServiceTestBase : IDisposable
    {
        protected readonly LunchUpContext LunchUpContext;
        

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

using LunchUp.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Options;

namespace LunchUp.Model
{
    public class LunchUpContext : DbContext
    {
        private ILoggerFactory _loggerFactory;

        public LunchUpContext(DbContextOptions<LunchUpContext> options, ILoggerFactory loggerFactory) : base(options)
        {
            _loggerFactory = loggerFactory;
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(_loggerFactory) //tie-up DbContext with LoggerFactory object
                .EnableSensitiveDataLogging();
        }

        public DbSet<PersonEntity> Person { get; set; }
        public DbSet<ResponseEntity> Response { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PersonEntity>()
                .HasMany(c => c.Responses)
                .WithOne(e => e.Origin);
        }
    }
}
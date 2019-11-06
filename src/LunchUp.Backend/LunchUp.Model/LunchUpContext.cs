using LunchUp.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace LunchUp.Model
{
    public class LunchUpContext : DbContext
    {
        public LunchUpContext(DbContextOptions<LunchUpContext> options) : base(options)
        {
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
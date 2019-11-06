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

    }
}
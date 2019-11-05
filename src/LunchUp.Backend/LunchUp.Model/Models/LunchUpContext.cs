using Microsoft.EntityFrameworkCore;

namespace LunchUp.Model.Models
{
    public class LunchUpContext:DbContext
    {
        public LunchUpContext(DbContextOptions<LunchUpContext> options):base(options) {  }
        public DbSet<PersonEntity> Person { get; set; }
        public DbSet<ResponseEntity> Response { get; set; }
    }
}
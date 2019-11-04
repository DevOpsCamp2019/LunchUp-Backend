using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;

namespace LunchUp.Model
{
    public class LunchUpContext:DbContext
    {
        public MyWebApiContext(DbContextOptions<LunchUpContext> options):base(options) {  }
        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }
    }
}
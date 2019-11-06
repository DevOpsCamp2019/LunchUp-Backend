using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace LunchUp.Model
{
    // ReSharper disable once UnusedMember.Global
    public class LunchUpContextFactory : IDesignTimeDbContextFactory<LunchUpContext>
    {
        public LunchUpContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<LunchUpContext>();
            optionsBuilder.UseNpgsql("User ID=lunchup;Password=lunchup;Server=localhost;Port=5432;Database=lunchup;Integrated Security=true;Pooling=true;");
            return new LunchUpContext(optionsBuilder.Options);
        }
    }
}
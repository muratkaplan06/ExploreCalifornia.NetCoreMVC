using Microsoft.EntityFrameworkCore;

namespace ExploreCalifornia.Models
{
    public class BlogDbContext:DbContext
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> options):base(options)
        {
            Database.EnsureCreated();
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }
        public DbSet<Post> Posts { get; set; }
        public DbSet<MonthlySpecial> MonthlySpecials { get; set; }
    }
}

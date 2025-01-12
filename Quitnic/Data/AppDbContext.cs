namespace Quitnic.Data
{
    using Microsoft.EntityFrameworkCore;

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Models.User> User { get; set; }
        public DbSet<Models.UserSmokeHistory> UserSmokeHistory { get; set; }
    }
}

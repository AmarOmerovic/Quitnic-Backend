namespace Quitnic.Data
{
    using Microsoft.EntityFrameworkCore;
    using Quitnic.Models;

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Models.User> User { get; set; }
        public DbSet<Models.UserSmokeHistory> UserSmokeHistory { get; set; }
        public DbSet<MotivationTip> MotivationTip { get; set; } 
    }
}

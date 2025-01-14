namespace Quitnic.Data
{
    using Microsoft.EntityFrameworkCore;
    using Quitnic.Models.Achievement;
    using Quitnic.Models.MotivationTip;
    using Quitnic.Models.User;
    using Quitnic.Models.UserSmokeHistory;

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<UserModel> User { get; set; }
        public DbSet<UserSmokeHistoryModel> UserSmokeHistory { get; set; }
        public DbSet<MotivationTipModel> MotivationTip { get; set; }
        public DbSet<AchievementModel> Achievement { get; set; }
        public DbSet<UserAchievementModel> UserAchievement { get; set; }
    }
}

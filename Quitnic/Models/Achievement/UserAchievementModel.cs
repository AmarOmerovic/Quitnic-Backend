namespace Quitnic.Models.Achievement
{
    public class UserAchievementModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required Guid UserId { get; set; }
        public required Guid AchievementId { get; set; }
        public DateTime UnlockedAt { get; set; } = DateTime.UtcNow;
        public AchievementModel Achievement { get; set; } = null!;
    }
}

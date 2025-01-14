namespace Quitnic.Models
{
    public class AchievementStatus
    {
        public Achievement Achievement { get; set; } = null!;
        public bool IsUnlocked { get; set; }
        public DateTime? UnlockedAt { get; set; }
    }
}

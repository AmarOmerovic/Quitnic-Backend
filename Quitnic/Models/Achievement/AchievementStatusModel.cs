namespace Quitnic.Models.Achievement
{
    public class AchievementStatusModel
    {
        public AchievementModel Achievement { get; set; } = null!;
        public bool IsUnlocked { get; set; }
        public DateTime? UnlockedAt { get; set; }
    }
}

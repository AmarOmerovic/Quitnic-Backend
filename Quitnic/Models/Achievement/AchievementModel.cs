namespace Quitnic.Models.Achievement
{
    public class AchievementModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string Title { get; set; }
        public required string Criteria { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}

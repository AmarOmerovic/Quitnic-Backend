namespace Quitnic.Models
{
    public class Achievement
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string Title { get; set; }
        public required string Criteria { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}

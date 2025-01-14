namespace Quitnic.Models.UserSmokeHistory
{
    public class UserSmokeHistoryModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; }
        public required DateTime QuitDate { get; set; }
        public required double CostPerPack { get; set; }
        public required string ReasonForQuitting { get; set; }
        public required int PacksPerDay { get; set; }
        public required int CigarettesPerPack { get; set; }
        public int CigarettesPerDay { get; set; }
    }
}

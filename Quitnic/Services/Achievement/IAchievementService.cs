using Quitnic.Models.Achievement;

namespace Quitnic.Services.Achievement
{
    public interface IAchievementService
    {
        Task<IEnumerable<AchievementStatusModel>> GetAchievementsForUserAsync(Guid userId);
    }
}

using Quitnic.Models.Achievement;
using Quitnic.Repositories.Base;

namespace Quitnic.Repositories.Achievement.UserAchievement
{
    public interface IUserAchievementRepository : IBaseRepository<UserAchievementModel>
    {
        Task<IEnumerable<UserAchievementModel>> GetUserAchievementsAsync(Guid userId);
    }
}

using Quitnic.Data;
using Quitnic.Models.Achievement;
using Quitnic.Repositories.Base;

namespace Quitnic.Repositories.Achievement.CoreAchievement
{
    public class AchievementRepository : BaseRepository<AchievementModel>, IAchievementRepository
    {
        public AchievementRepository(AppDbContext context) : base(context)
        {
        }
    }
}

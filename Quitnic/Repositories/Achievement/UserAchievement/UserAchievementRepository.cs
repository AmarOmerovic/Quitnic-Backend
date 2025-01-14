using Microsoft.EntityFrameworkCore;
using Quitnic.Data;
using Quitnic.Models.Achievement;
using Quitnic.Repositories.Base;

namespace Quitnic.Repositories.Achievement.UserAchievement
{
    public class UserAchievementRepository : BaseRepository<UserAchievementModel>, IUserAchievementRepository
    {
        public UserAchievementRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<UserAchievementModel>> GetUserAchievementsAsync(Guid userId)
        {
            return await _context.UserAchievement
                .Include(ua => ua.Achievement)
                .Where(ua => ua.UserId == userId)
                .ToListAsync();
        }
    }
}

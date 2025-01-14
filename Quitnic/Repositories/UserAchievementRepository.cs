using Microsoft.EntityFrameworkCore;
using Quitnic.Data;
using Quitnic.Models;

namespace Quitnic.Repositories
{
    public interface IUserAchievementRepository
    {
        Task<IEnumerable<UserAchievement>> GetUserAchievementsAsync(Guid userId);
        Task AddUserAchievementAsync(UserAchievement userAchievement);
    }

    public class UserAchievementRepository : IUserAchievementRepository
    {
        private readonly AppDbContext _context;

        public UserAchievementRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserAchievement>> GetUserAchievementsAsync(Guid userId)
        {
            return await _context.UserAchievement
                .Include(ua => ua.Achievement) // Include related Achievement data
                .Where(ua => ua.UserId == userId)
                .ToListAsync();
        }

        public async Task AddUserAchievementAsync(UserAchievement userAchievement)
        {
            _context.UserAchievement.Add(userAchievement);
            await _context.SaveChangesAsync();
        }
    }
}

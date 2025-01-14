using Microsoft.EntityFrameworkCore;
using Quitnic.Data;
using Quitnic.Models;

namespace Quitnic.Repositories
{
    public interface IAchievementRepository
    {
        Task<IEnumerable<Achievement>> GetAllAchievementsAsync();
    }

    public class AchievementRepository : IAchievementRepository
    {
        private readonly AppDbContext _context;

        public AchievementRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Achievement>> GetAllAchievementsAsync()
        {
            return await _context.Achievement.ToListAsync();
        }
    }
}

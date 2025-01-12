using Quitnic.Data;
using Quitnic.Models;
using Microsoft.EntityFrameworkCore;

namespace Quitnic.Repositories
{
    public interface IUserSmokeHistoryRepository
    {
        Task<UserSmokeHistory?> GetByUserIdAsync(Guid userId);
        Task AddAsync(UserSmokeHistory history);
        Task UpdateAsync(UserSmokeHistory history);
    }

    public class UserSmokeHistoryRepository : IUserSmokeHistoryRepository
    {
        private readonly AppDbContext _context;

        public UserSmokeHistoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<UserSmokeHistory?> GetByUserIdAsync(Guid userId)
        {
            return await _context.UserSmokeHistory
                .FirstOrDefaultAsync(h => h.UserId == userId);
        }

        public async Task AddAsync(UserSmokeHistory history)
        {
            _context.UserSmokeHistory.Add(history);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(UserSmokeHistory history)
        {
            var existingHistory = await GetByUserIdAsync(history.UserId);

            if (existingHistory != null)
            {
                _context.UserSmokeHistory.Update(existingHistory);
                await _context.SaveChangesAsync();
            }
        }

    }
}

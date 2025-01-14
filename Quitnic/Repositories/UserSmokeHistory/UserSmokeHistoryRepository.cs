using Microsoft.EntityFrameworkCore;
using Quitnic.Data;
using Quitnic.Models.UserSmokeHistory;
using Quitnic.Repositories.Base;

namespace Quitnic.Repositories.UserSmokeHistory
{
    public class UserSmokeHistoryRepository : BaseRepository<UserSmokeHistoryModel>, IUserSmokeHistoryRepository
    {
        public UserSmokeHistoryRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<UserSmokeHistoryModel?> GetByUserIdAsync(Guid userId)
        {
            return await _context.UserSmokeHistory
                .FirstOrDefaultAsync(h => h.UserId == userId);
        }
    }
}

using Quitnic.Models.UserSmokeHistory;
using Quitnic.Repositories.Base;

namespace Quitnic.Repositories.UserSmokeHistory
{
    public interface IUserSmokeHistoryRepository : IBaseRepository<UserSmokeHistoryModel>
    {
        Task<UserSmokeHistoryModel?> GetByUserIdAsync(Guid userId);
    }
}

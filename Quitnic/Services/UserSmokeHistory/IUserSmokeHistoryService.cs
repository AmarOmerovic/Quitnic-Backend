using Quitnic.Models.UserSmokeHistory;

namespace Quitnic.Services.UserSmokeHistory
{
    public interface IUserSmokeHistoryService
    {
        Task<UserSmokeHistoryModel?> GetSmokeHistoryByUserIdAsync(Guid userId);
        Task<bool> UpdateSmokeHistoryAsync(Guid userId, UserSmokeHistoryModel updatedHistory);
    }
}

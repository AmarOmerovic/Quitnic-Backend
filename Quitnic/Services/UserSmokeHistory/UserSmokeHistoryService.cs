using Quitnic.Models.UserSmokeHistory;
using Quitnic.Repositories.Base;
using Quitnic.Services.Base;

namespace Quitnic.Services.UserSmokeHistory
{
    public class UserSmokeHistoryService : BaseService, IUserSmokeHistoryService
    {
        private readonly IBaseRepository<UserSmokeHistoryModel> _repository;

        public UserSmokeHistoryService(IBaseRepository<UserSmokeHistoryModel> repository)
        {
            _repository = repository;
        }

        public async Task<UserSmokeHistoryModel?> GetSmokeHistoryByUserIdAsync(Guid userId)
        {
            LogInfo($"Fetching smoke history for User ID: {userId}");

            var histories = await _repository.FindAsync(h => h.UserId == userId);
            var history = histories.FirstOrDefault();

            if (history == null)
            {
                LogInfo($"No smoke history found for User ID: {userId}");
            }

            return history;
        }

        public async Task<bool> UpdateSmokeHistoryAsync(Guid userId, UserSmokeHistoryModel updatedHistory)
        {
            if (!ValidateEntity(updatedHistory))
            {
                throw new ArgumentException("Invalid smoke history data.");
            }

            LogInfo($"Updating smoke history for User ID: {userId}");

            var existingHistory = await GetSmokeHistoryByUserIdAsync(userId);

            if (existingHistory == null)
            {
                LogInfo($"No existing smoke history for User ID: {userId}. Creating new record.");

                updatedHistory.UserId = userId;
                updatedHistory.CigarettesPerDay = CalculateCigarettesPerDay(updatedHistory);
                await _repository.AddAsync(updatedHistory);
                return true;
            }

            existingHistory.QuitDate = updatedHistory.QuitDate;
            existingHistory.CostPerPack = updatedHistory.CostPerPack;
            existingHistory.PacksPerDay = updatedHistory.PacksPerDay;
            existingHistory.CigarettesPerPack = updatedHistory.CigarettesPerPack;
            existingHistory.ReasonForQuitting = updatedHistory.ReasonForQuitting;
            existingHistory.CigarettesPerDay = CalculateCigarettesPerDay(updatedHistory);

            await _repository.UpdateAsync(existingHistory);

            LogInfo($"Successfully updated smoke history for User ID: {userId}");
            return true;
        }

        private int CalculateCigarettesPerDay(UserSmokeHistoryModel history)
        {
            return history.PacksPerDay * history.CigarettesPerPack;
        }
    }
}

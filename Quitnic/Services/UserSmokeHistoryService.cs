using Quitnic.Models;
using Quitnic.Repositories;

namespace Quitnic.Services
{
    public interface IUserSmokeHistoryService
    {
        Task<UserSmokeHistory?> GetSmokeHistoryByUserIdAsync(Guid userId);
        Task<bool> UpdateSmokeHistoryAsync(Guid userId, UserSmokeHistory updatedHistory);
    }

    public class UserSmokeHistoryService : IUserSmokeHistoryService
    {
        private readonly IUserSmokeHistoryRepository _repository;

        public UserSmokeHistoryService(IUserSmokeHistoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<UserSmokeHistory?> GetSmokeHistoryByUserIdAsync(Guid userId)
        {
            return await _repository.GetByUserIdAsync(userId);
        }

        public async Task<bool> UpdateSmokeHistoryAsync(Guid userId, UserSmokeHistory updatedHistory)
        {
            var existingHistory = await _repository.GetByUserIdAsync(userId);
            if (existingHistory == null)
            {
                updatedHistory.UserId = userId;
                updatedHistory.CigarettesPerDay = updatedHistory.PacksPerDay * updatedHistory.CigarettesPerPack;
                await _repository.AddAsync(updatedHistory);
                return true;
            }

            existingHistory.QuitDate = updatedHistory.QuitDate;
            existingHistory.CostPerPack = updatedHistory.CostPerPack;
            existingHistory.PacksPerDay = updatedHistory.PacksPerDay;
            existingHistory.CigarettesPerPack = updatedHistory.CigarettesPerPack;
            existingHistory.ReasonForQuitting = updatedHistory.ReasonForQuitting;
            existingHistory.CigarettesPerDay = updatedHistory.PacksPerDay * updatedHistory.CigarettesPerPack;
            await _repository.UpdateAsync(existingHistory);
            return true;
        }
    }
}

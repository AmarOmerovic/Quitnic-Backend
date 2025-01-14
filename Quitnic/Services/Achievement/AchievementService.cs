using Quitnic.Helpers;
using Quitnic.Models.Achievement;
using Quitnic.Models.UserSmokeHistory;
using Quitnic.Repositories.Base;
using Quitnic.Repositories.UserSmokeHistory;
using Quitnic.Services.Base;
using System.Text.Json;

namespace Quitnic.Services.Achievement
{
    public class AchievementService : BaseService, IAchievementService
    {
        private readonly IBaseRepository<AchievementModel> _achievementRepository;
        private readonly IBaseRepository<UserAchievementModel> _userAchievementRepository;
        private readonly IUserSmokeHistoryRepository _smokeHistoryRepository;

        public AchievementService(
            IBaseRepository<AchievementModel> achievementRepository,
            IBaseRepository<UserAchievementModel> userAchievementRepository,
            IUserSmokeHistoryRepository smokeHistoryRepository)
        {
            _achievementRepository = achievementRepository;
            _userAchievementRepository = userAchievementRepository;
            _smokeHistoryRepository = smokeHistoryRepository;
        }

        public async Task<IEnumerable<AchievementStatusModel>> GetAchievementsForUserAsync(Guid userId)
        {
            LogInfo($"Fetching achievements for user {userId}");

            var achievements = await _achievementRepository.GetAllAsync();
            var unlockedAchievements = await _userAchievementRepository.FindAsync(ua => ua.UserId == userId);
            var smokeHistory = await _smokeHistoryRepository.GetByUserIdAsync(userId);

            if (!ValidateEntity(smokeHistory))
            {
                throw new InvalidOperationException("User smoke history not found.");
            }

            var userProgress = QuitnicHelper.Calculate(smokeHistory);

            // Process achievements concurrently
            var achievementStatuses = await BackgroundTaskHelper.RunBackgroundTask(() =>
                Task.FromResult(EvaluateAndUpdateAchievements(userId, achievements, unlockedAchievements, userProgress)),
                ex => LogError($"Error processing achievements: {ex.Message}")
            );

            return achievementStatuses ?? Enumerable.Empty<AchievementStatusModel>();
        }


        private IEnumerable<AchievementStatusModel> EvaluateAndUpdateAchievements(
            Guid userId,
            IEnumerable<AchievementModel> achievements,
            IEnumerable<UserAchievementModel> unlockedAchievements,
            UserProgressModel userProgress)
        {
            LogInfo($"Evaluating and updating achievements for user {userId}");

            var unlockedIds = unlockedAchievements.Select(ua => ua.AchievementId).ToHashSet();
            var achievementStatuses = new List<AchievementStatusModel>();

            foreach (var achievement in achievements)
            {
                var isUnlocked = unlockedIds.Contains(achievement.Id);

                if (!isUnlocked && EvaluateAchievement(achievement, userProgress))
                {
                    _userAchievementRepository.AddAsync(new UserAchievementModel
                    {
                        UserId = userId,
                        AchievementId = achievement.Id
                    }).Wait();
                    isUnlocked = true;

                    LogInfo($"Achievement unlocked: {achievement.Title} for user {userId}");
                }

                achievementStatuses.Add(new AchievementStatusModel
                {
                    Achievement = achievement,
                    IsUnlocked = isUnlocked,
                    UnlockedAt = unlockedAchievements
                        .FirstOrDefault(ua => ua.AchievementId == achievement.Id)?.UnlockedAt
                });
            }

            return achievementStatuses;
        }

        private bool EvaluateAchievement(AchievementModel achievement, UserProgressModel progress)
        {
            var criteria = JsonSerializer.Deserialize<AchievementCriteriaModel>(achievement.Criteria);
            if (criteria == null || string.IsNullOrEmpty(criteria.Type))
            {
                LogError($"Invalid or missing criteria for achievement {achievement.Id}");
                throw new InvalidOperationException("Invalid or missing criteria.");
            }

            return criteria.Type.ToLower() switch
            {
                "days_smoke_free" => progress.DaysSmokeFree >= criteria.Value,
                "money_saved" => progress.MoneySaved >= criteria.Value,
                "cigarettes_avoided" => progress.CigarettesAvoided >= criteria.Value,
                _ => false
            };
        }
    }
}

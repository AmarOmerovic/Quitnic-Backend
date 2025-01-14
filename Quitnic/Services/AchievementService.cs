using Quitnic.Models;
using Quitnic.Repositories;
using System.Text.Json;

namespace Quitnic.Services
{
    public interface IAchievementService
    {
        Task<IEnumerable<AchievementStatus>> GetAchievementsForUserAsync(Guid userId);
    }

    public class AchievementService : IAchievementService
    {
        private readonly IAchievementRepository _achievementRepository;
        private readonly IUserAchievementRepository _userAchievementRepository;
        private readonly IUserSmokeHistoryRepository _smokeHistoryRepository;

        private readonly ILogger<AchievementService> _logger;


        public AchievementService(
            IAchievementRepository achievementRepository,
            IUserAchievementRepository userAchievementRepository,
            IUserSmokeHistoryRepository smokeHistoryRepository,
            ILogger<AchievementService> logger
            )
        {
            _achievementRepository = achievementRepository;
            _userAchievementRepository = userAchievementRepository;
            _smokeHistoryRepository = smokeHistoryRepository;
            _logger = logger;

        }

        public async Task<IEnumerable<AchievementStatus>> GetAchievementsForUserAsync(Guid userId)
        {
            // Step 1: Fetch all achievements
            var achievements = await _achievementRepository.GetAllAchievementsAsync();

            // Step 2: Fetch unlocked achievements for the user
            var unlockedAchievements = await _userAchievementRepository.GetUserAchievementsAsync(userId);

            // Step 3: Fetch user's smoke history
            var smokeHistory = await _smokeHistoryRepository.GetByUserIdAsync(userId);
            if (smokeHistory == null)
            {
                throw new InvalidOperationException("User smoke history not found.");
            }

            // Step 4: Calculate user's progress
            var userProgress = CalculateUserProgress(smokeHistory);

            // Step 5: Evaluate achievements and prepare response
            var unlockedIds = unlockedAchievements.Select(ua => ua.AchievementId).ToHashSet();
            var achievementStatuses = new List<AchievementStatus>();

            foreach (var achievement in achievements)
            {
                var isUnlocked = unlockedIds.Contains(achievement.Id);

                // Evaluate locked achievements
                if (!isUnlocked && EvaluateAchievement(achievement, userProgress))
                {
                    // Unlock the achievement
                    await _userAchievementRepository.AddUserAchievementAsync(new UserAchievement
                    {
                        UserId = userId,
                        AchievementId = achievement.Id
                    });
                    isUnlocked = true;
                }

                // Add to response
                achievementStatuses.Add(new AchievementStatus
                {
                    Achievement = achievement,
                    IsUnlocked = isUnlocked,
                    UnlockedAt = unlockedAchievements
                        .FirstOrDefault(ua => ua.AchievementId == achievement.Id)?.UnlockedAt
                });
            }

            return achievementStatuses;
        }

        private UserProgress CalculateUserProgress(UserSmokeHistory smokeHistory)
        {
            var daysSmokeFree = (DateTime.UtcNow - smokeHistory.QuitDate).Days;
            var cigarettesAvoided = daysSmokeFree * smokeHistory.CigarettesPerDay;
            var moneySaved = daysSmokeFree * smokeHistory.CostPerPack;

            return new UserProgress
            {
                DaysSmokeFree = daysSmokeFree,
                CigarettesAvoided = cigarettesAvoided,
                MoneySaved = moneySaved
            };
        }

        private bool EvaluateAchievement(Achievement achievement, UserProgress progress)
        {
            // Parse the Criteria JSON and compare with user progress.
            // Example: {"type": "days_smoke_free", "value": 30}

            var criteria = JsonSerializer.Deserialize<AchievementCriteria>(achievement.Criteria);
            return criteria switch
            {
                { Type: "days_smoke_free" } => progress.DaysSmokeFree >= criteria.Value,
                { Type: "money_saved" } => progress.MoneySaved >= criteria.Value,
                { Type: "cigarettes_avoided" } => progress.CigarettesAvoided >= criteria.Value,
                _ => false
            };
        }
    }
}

using Quitnic.Models.UserSmokeHistory;

namespace Quitnic.Helpers
{
    public static class QuitnicHelper
    {
        public static UserProgressModel Calculate(UserSmokeHistoryModel smokeHistory)
        {
            var daysSmokeFree = (DateTime.UtcNow - smokeHistory.QuitDate).Days;
            var cigarettesAvoided = daysSmokeFree * smokeHistory.CigarettesPerDay;
            var moneySaved = daysSmokeFree * smokeHistory.CostPerPack;

            return new UserProgressModel
            {
                DaysSmokeFree = daysSmokeFree,
                CigarettesAvoided = cigarettesAvoided,
                MoneySaved = moneySaved
            };
        }
    }
}

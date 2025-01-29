using Quitnic.Models.UserSmokeHistory;

namespace Quitnic.Helpers
{
    public sealed class QuitnicHelper
    {
        private static readonly Lazy<QuitnicHelper> _instance =
            new Lazy<QuitnicHelper>(() => new QuitnicHelper());

        public static QuitnicHelper Instance => _instance.Value;

        private QuitnicHelper() { }

        public UserProgressModel Calculate(UserSmokeHistoryModel smokeHistory)
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

        public async Task<T?> RunBackgroundTask<T>(Func<Task<T>> taskFunc, Action<Exception> onError)
        {
            try
            {
                return await Task.Run(taskFunc);
            }
            catch (Exception ex)
            {
                onError?.Invoke(ex);
                return default;
            }
        }
    }
}

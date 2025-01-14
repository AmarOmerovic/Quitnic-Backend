namespace Quitnic.Helpers
{
    public static class BackgroundTaskHelper
    {
        public static async Task<T?> RunBackgroundTask<T>(Func<Task<T>> taskFunc, Action<Exception> onError)
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

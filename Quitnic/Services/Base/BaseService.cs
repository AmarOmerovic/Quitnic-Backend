namespace Quitnic.Services.Base
{
    public abstract class BaseService : IBaseService
    {
        public void LogInfo(string message)
        {
            Console.WriteLine($"INFO: {message}");
        }

        public void LogError(string message)
        {
            Console.WriteLine($"ERROR: {message}");
        }

        public bool ValidateEntity(object entity)
        {
            if (entity == null)
            {
                LogError("Entity validation failed: entity is null.");
                return false;
            }
            return true;
        }
    }
}

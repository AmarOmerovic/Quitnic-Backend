namespace Quitnic.Services.Base
{
    public interface IBaseService
    {
        void LogInfo(string message);
        void LogError(string message);
        bool ValidateEntity(object entity);
    }
}

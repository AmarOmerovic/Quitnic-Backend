using Quitnic.Models.User;

namespace Quitnic.Services.User
{
    public interface IUserService
    {
        Task<UserModel?> GetUserByIdAsync(Guid id);
        Task<UserModel> CreateUserAsync(UserModel user);
    }
}

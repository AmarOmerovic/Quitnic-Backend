using Quitnic.Models.User;
using Quitnic.Repositories.Base;
using Quitnic.Services.Base;

namespace Quitnic.Services.User
{
    public class UserService : BaseService, IUserService
    {
        private readonly IBaseRepository<UserModel> _repository;

        public UserService(IBaseRepository<UserModel> repository)
        {
            _repository = repository;
        }

        public async Task<UserModel?> GetUserByIdAsync(Guid id)
        {
            LogInfo($"Fetching user with ID: {id}");

            var user = await _repository.GetByIdAsync(id);

            if (user == null)
            {
                LogInfo($"No user found with ID: {id}");
            }

            return user;
        }

        public async Task<UserModel> CreateUserAsync(UserModel user)
        {
            if (!ValidateEntity(user))
            {
                throw new ArgumentException("Invalid user data.");
            }

            LogInfo($"Creating user with email: {user.Email}");

            await _repository.AddAsync(user);

            LogInfo($"User created with ID: {user.Id}");

            return user;
        }
    }
}

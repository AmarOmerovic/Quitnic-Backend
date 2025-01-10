using Quitnic.Models;
using Quitnic.Repositories;

namespace Quitnic.Services
{
    public interface IUserService
    {
        Task<User?> GetUserByIdAsync(Guid id);
        Task<User> CreateUserAsync(User user);
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<User?> GetUserByIdAsync(Guid id)
        {
            return await _repository.GetUserByIdAsync(id);
        }

        public async Task<User> CreateUserAsync(User user)
        {
            return await _repository.CreateUserAsync(user);
        }
    }
}

using Quitnic.Data;
using Quitnic.Models.User;
using Quitnic.Repositories.Base;

namespace Quitnic.Repositories.User
{
    public class UserRepository : BaseRepository<UserModel>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }
    }
}

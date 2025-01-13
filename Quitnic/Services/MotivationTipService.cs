using Quitnic.Models;
using Quitnic.Repositories;

namespace Quitnic.Services
{
    public interface IMotivationTipService
    {
        Task<IEnumerable<MotivationTip>> GetAllMotivationTipsAsync();
    }

    public class MotivationTipService : IMotivationTipService
    {
        private readonly IMotivationTipRepository _repository;

        public MotivationTipService(IMotivationTipRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<MotivationTip>> GetAllMotivationTipsAsync()
        {
            return await _repository.GetAllMotivationTipsAsync();
        }
    }
}

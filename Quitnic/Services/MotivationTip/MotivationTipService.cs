using Quitnic.Models.MotivationTip;
using Quitnic.Repositories.Base;
using Quitnic.Services.Base;

namespace Quitnic.Services.MotivationTip
{
    public class MotivationTipService : BaseService, IMotivationTipService
    {
        private readonly IBaseRepository<MotivationTipModel> _repository;

        public MotivationTipService(IBaseRepository<MotivationTipModel> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<MotivationTipModel>> GetAllMotivationTipsAsync()
        {
            LogInfo("Fetching all motivation tips.");

            var tips = await _repository.GetAllAsync();

            if (!tips.Any())
            {
                LogInfo("No motivation tips found.");
            }

            return tips;
        }
    }
}

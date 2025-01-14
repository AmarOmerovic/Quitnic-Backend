using Quitnic.Models.MotivationTip;

namespace Quitnic.Services.MotivationTip
{
    public interface IMotivationTipService
    {
        Task<IEnumerable<MotivationTipModel>> GetAllMotivationTipsAsync();
    }
}

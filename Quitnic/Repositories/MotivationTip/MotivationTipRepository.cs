using Quitnic.Data;
using Quitnic.Models.MotivationTip;
using Quitnic.Repositories.Base;

namespace Quitnic.Repositories.MotivationTip
{
    public class MotivationTipRepository : BaseRepository<MotivationTipModel>, IMotivationTipRepository
    {
        public MotivationTipRepository(AppDbContext context) : base(context)
        {
        }
    }
}

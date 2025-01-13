using Microsoft.EntityFrameworkCore;
using Quitnic.Data;
using Quitnic.Models;

namespace Quitnic.Repositories
{
    public interface IMotivationTipRepository
    {
        Task<IEnumerable<MotivationTip>> GetAllMotivationTipsAsync();
    }

    public class MotivationTipRepository : IMotivationTipRepository
    {
        private readonly AppDbContext _context;

        public MotivationTipRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MotivationTip>> GetAllMotivationTipsAsync()
        {
            return await _context.Set<MotivationTip>().ToListAsync();
        }
    }
}

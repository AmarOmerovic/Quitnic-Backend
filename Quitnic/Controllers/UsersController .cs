using Microsoft.AspNetCore.Mvc;

namespace Quitnic.Controllers
{
    [ApiController]
    [Route("api/test")]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult TestDatabase()
        {
            var userCount = _context.Users.Count();
            return Ok($"Database is connected! Users count: {userCount}");
        }
    }
}

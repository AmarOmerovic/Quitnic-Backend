using Microsoft.AspNetCore.Mvc;
using Quitnic.Services;

namespace Quitnic.Controllers
{
    [ApiController]
    [Route("api/motivation-tips")]
    public class MotivationTipController : ControllerBase
    {
        private readonly IMotivationTipService _service;

        public MotivationTipController(IMotivationTipService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMotivationTips()
        {
            var tips = await _service.GetAllMotivationTipsAsync();
            return Ok(tips);
        }
    }
}

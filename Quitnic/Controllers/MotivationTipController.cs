using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quitnic.Services.MotivationTip;

namespace Quitnic.Controllers
{
    [ApiController]
    [Route("api/motivation-tips")]
    [Authorize]
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
            try
            {
                var tips = await _service.GetAllMotivationTipsAsync();

                if (!tips.Any())
                {
                    return NotFound(new { message = "No motivation tips available." });
                }

                return Ok(tips);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }

    }
}

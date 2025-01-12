using Microsoft.AspNetCore.Mvc;
using Quitnic.Models;
using Quitnic.Services;

namespace Quitnic.Controllers
{
    [ApiController]
    [Route("api/smoke-history")]
    public class SmokeHistoryController : ControllerBase
    {
        private readonly IUserSmokeHistoryService _service;

        public SmokeHistoryController(IUserSmokeHistoryService service)
        {
            _service = service;
        }

        // GET: /api/smoke-history/{userId}
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetSmokeHistory(Guid userId)
        {
            var history = await _service.GetSmokeHistoryByUserIdAsync(userId);

            if (history == null)
            {
                return NotFound(new { message = "Smoke history not found." });
            }

            return Ok(history);
        }

        // PUT: /api/smoke-history/{userId}
        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateSmokeHistory(Guid userId, [FromBody] UserSmokeHistory updatedHistory)
        {
            if (updatedHistory.UserId != Guid.Empty) return StatusCode(400, new { message = "UserId should not be passed in the body." });

            var result = await _service.UpdateSmokeHistoryAsync(userId, updatedHistory);

            if (result)
            {
                return Ok(new
                {
                    message = "Smoke history created or updated successfully.",
                    cigarettesPerDay = updatedHistory.CigarettesPerDay
                });
            }

            return StatusCode(500, new { message = "An error occurred while updating smoke history." });
        }

    }
}

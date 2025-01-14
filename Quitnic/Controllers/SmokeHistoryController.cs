using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quitnic.Models.UserSmokeHistory;
using Quitnic.Services.UserSmokeHistory;

namespace Quitnic.Controllers
{
    [ApiController]
    [Route("api/smoke-history")]
    [Authorize]
    public class SmokeHistoryController : ControllerBase
    {
        private readonly IUserSmokeHistoryService _service;

        public SmokeHistoryController(IUserSmokeHistoryService service)
        {
            _service = service;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetSmokeHistory(Guid userId)
        {
            try
            {
                var history = await _service.GetSmokeHistoryByUserIdAsync(userId);

                if (history == null)
                {
                    return NotFound(new { message = "Smoke history not found." });
                }

                return Ok(history);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateSmokeHistory(Guid userId, [FromBody] UserSmokeHistoryModel updatedHistory)
        {
            try
            {
                if (userId == Guid.Empty)
                {
                    return BadRequest(new { message = "Invalid user ID." });
                }

                if (updatedHistory == null)
                {
                    return BadRequest(new { message = "Request body cannot be null." });
                }

                if (updatedHistory.UserId != Guid.Empty)
                {
                    return BadRequest(new { message = "UserId should not be included in the request body." });
                }

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
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }
    }
}

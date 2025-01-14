using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quitnic.Services.Achievement;

namespace Quitnic.Controllers
{
    [ApiController]
    [Route("api/achievements")]
    [Authorize]
    public class AchievementsController : ControllerBase
    {
        private readonly IAchievementService _achievementService;

        public AchievementsController(IAchievementService achievementService)
        {
            _achievementService = achievementService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetAchievementsForUser(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                return BadRequest(new { message = "Invalid user ID." });
            }

            try
            {
                var achievements = await _achievementService.GetAchievementsForUserAsync(userId);

                if (!achievements.Any())
                {
                    return NotFound(new { message = "No achievements found for the user." });
                }

                return Ok(achievements);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }
    }

}

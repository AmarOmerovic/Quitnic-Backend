using Microsoft.AspNetCore.Mvc;
using Quitnic.Services;
using Quitnic.Models;

namespace Quitnic.Controllers
{
    [ApiController]
    [Route("api/achievements/{userId}")]
    public class AchievementsController : ControllerBase
    {
        private readonly IAchievementService _achievementService;

        public AchievementsController(IAchievementService achievementService)
        {
            _achievementService = achievementService;
        }

        /// <summary>
        /// Fetch all achievements for a user, including locked and unlocked status.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAchievementsForUser(Guid userId)
        {
            try
            {
                var achievements = await _achievementService.GetAchievementsForUserAsync(userId);
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

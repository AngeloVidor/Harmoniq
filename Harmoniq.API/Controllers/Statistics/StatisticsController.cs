using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Harmoniq.BLL.Interfaces.Stats;
using Harmoniq.BLL.Interfaces.UserContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Harmoniq.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "ContentCreator")]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticsService _statisticsService;
        private readonly IUserContextService _userContextService;

        public StatisticsController(IStatisticsService statisticsService, IUserContextService userContextService)
        {
            _statisticsService = statisticsService;
            _userContextService = userContextService;
        }

        [HttpGet("stats")]
        public async Task<IActionResult> GetStatisticsAsync(int year, int month)
        {
            var userId = _userContextService.GetUserIdFromContext();
            var creatorId = await _userContextService.GetContentCreatorIdByUserIdAsync(userId);

            try
            {
                var result = await _statisticsService.GetMonthlyStatisticsAsync(year, month, creatorId);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return StatusCode(400, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
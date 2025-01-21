using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Harmoniq.BLL.DTOs;
using Harmoniq.BLL.Interfaces.Follows;
using Harmoniq.BLL.Interfaces.UserContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Harmoniq.API.Controllers.Follows
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "ContentConsumer")]
    public class FollowsController : ControllerBase
    {
        private readonly IFollowsService _followsService;
        private readonly IUserContextService _userContextService;

        public FollowsController(IFollowsService followsService, IUserContextService userContextService)
        {
            _followsService = followsService;
            _userContextService = userContextService;
        }

        [HttpPost("follow")]
        public async Task<IActionResult> FollowAsync([FromBody] FollowersDto follow)
        {
            var userId = _userContextService.GetUserIdFromContext();
            follow.FollowerConsumerId = (int)await _userContextService.GetContentConsumerIdByUserIdAsync(userId);
        
            try
            {
                var followed = await _followsService.FollowAsync(follow);
                return Ok(followed);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Harmoniq.BLL.DTOs;
using Harmoniq.BLL.Interfaces.ContentConsumerAccount;
using Harmoniq.BLL.Interfaces.UserContext;
using Microsoft.AspNetCore.Mvc;

namespace Harmoniq.API.Controllers.Profiles
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContentConsumerProfileController : ControllerBase
    {
        private readonly IContentConsumerAccountService _contentConsumerAccount;
        private readonly IUserContextService _userContextService;

        public ContentConsumerProfileController(IContentConsumerAccountService contentConsumerAccount, IUserContextService userContextService)
        {
            _contentConsumerAccount = contentConsumerAccount;
            _userContextService = userContextService;
        }


        [HttpPut("contentConsumer")]
        public async Task<IActionResult> UpdateConsumerProfile([FromBody] EditContentConsumerDto consumer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = _userContextService.GetUserIdFromContext();
            consumer.UserId = userId;
            var consumerId = _userContextService.GetUserIdFromContext();
            consumer.Id = (int)await _userContextService.GetContentConsumerIdByUserIdAsync(userId);

            try
            {
                var editedProfile = await _contentConsumerAccount.UpdateContentConsumerProfileAsync(consumer);
                return Ok(editedProfile);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }



        [HttpGet("contentConsumer/{contentConsumerId}")]
        public async Task<IActionResult> GetContentConsumerProfileAsync(int contentConsumerId)
        {
            try
            {
                var profile = await _contentConsumerAccount.GetContentConsumerProfileAsync(contentConsumerId);
                return Ok(profile);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
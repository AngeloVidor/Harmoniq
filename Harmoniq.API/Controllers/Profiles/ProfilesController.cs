using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Harmoniq.BLL.DTOs;
using Harmoniq.BLL.Interfaces.ContentConsumerAccount;
using Harmoniq.BLL.Interfaces.ContentCreatorAccount;
using Harmoniq.BLL.Interfaces.UserContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Harmoniq.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfilesController : ControllerBase
    {
        private readonly IContentConsumerAccountService _contentConsumerAccount;
        private readonly IUserContextService _userContextService;
        private readonly IContentCreatorProfileService _contentCreatorProfile;


        public ProfilesController(IContentConsumerAccountService contentConsumerAccount, IUserContextService userContextService, IContentCreatorProfileService contentCreatorProfile)
        {
            _contentConsumerAccount = contentConsumerAccount;
            _userContextService = userContextService;
            _contentCreatorProfile = contentCreatorProfile;
        }

        [HttpPost("ContentConsumer")]
        [Authorize(Roles = "ContentConsumer")]
        public async Task<IActionResult> AddContentConsumerProfile([FromBody] ContentConsumerDto consumerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            consumerDto.UserId = _userContextService.GetUserIdFromContext();

            try
            {
                var addedContentConsumer = await _contentConsumerAccount.AddContetConsumerAccountAsync(consumerDto);
                return Ok(addedContentConsumer);
            }
            catch (ValidationException ex)
            {
                return StatusCode(400, ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                return StatusCode(403, ex.Message);
            }
        }

        [HttpPost("ContentCreator")]
        [Authorize(Roles = "ContentCreator")]
        public async Task<IActionResult> AddContentCreatorProfile([FromBody] ContentCreatorDto contentCreatorDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            contentCreatorDto.UserId = _userContextService.GetUserIdFromContext();

            try
            {
                var contentCreator = await _contentCreatorProfile.AddContentCreatorProfile(contentCreatorDto);
                return Ok(contentCreator);
            }
            catch (ValidationException ex)
            {
                return StatusCode(400, ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                return StatusCode(403, ex.Message);
            }
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

        [HttpPut("contentCreator")]
        public async Task<IActionResult> UpdateContentCreatorProfileAsync([FromBody] EditContentCreatorProfileDto editContentCreator)
        {
            editContentCreator.UserId = _userContextService.GetUserIdFromContext();

            try
            {
                var editedProfile = await _contentCreatorProfile.EditContentCreatorProfileAsync(editContentCreator);
                return Ok(editedProfile);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("contentCreator/{contentCreatorId}")]
        public async Task<IActionResult> GetContentCreatorProfileAsync(int contentCreatorId)
        {
            try
            {
                var profile = await _contentCreatorProfile.GetContentCreatorProfileAsync(contentCreatorId);
                return Ok(profile);
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
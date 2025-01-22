using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Harmoniq.BLL.DTOs;
using Harmoniq.BLL.Interfaces.ContentCreatorAccount;
using Harmoniq.BLL.Interfaces.UserContext;
using Microsoft.AspNetCore.Mvc;

namespace Harmoniq.API.Controllers.Profiles
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContentCreatorProfileController : ControllerBase
    {

        private readonly IContentCreatorProfileService _contentCreatorProfile;
        private readonly IUserContextService _userContextService;

        public ContentCreatorProfileController(IContentCreatorProfileService contentCreatorProfile, IUserContextService userContextService)
        {
            _contentCreatorProfile = contentCreatorProfile;
            _userContextService = userContextService;
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
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Harmoniq.BLL.DTOs;
using Harmoniq.BLL.Interfaces.ContentCreatorAccount;
using Harmoniq.BLL.Interfaces.UserContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Harmoniq.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "ContentCreator")]
    public class ContentCreatorProfileController : ControllerBase
    {
        private readonly IContentCreatorProfileService _contentCreatorProfile;
        private readonly IUserContextService _userContextService;

        public ContentCreatorProfileController(IContentCreatorProfileService contentCreatorProfile, IUserContextService userContextService)
        {
            _contentCreatorProfile = contentCreatorProfile;
            _userContextService = userContextService;
        }


        [HttpPost("create-profile")]
        public async Task<IActionResult> CreateProfile([FromBody] ContentCreatorDto contentCreatorDto)
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

    }
}
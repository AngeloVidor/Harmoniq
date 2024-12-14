using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Harmoniq.BLL.DTOs;
using Harmoniq.BLL.Interfaces.ContentCreatorAccount;
using Microsoft.AspNetCore.Mvc;

namespace Harmoniq.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContentCreatorProfileController : ControllerBase
    {
        private readonly IContentCreatorProfileService _contentCreatorProfile;

        public ContentCreatorProfileController(IContentCreatorProfileService contentCreatorProfile)
        {
            _contentCreatorProfile = contentCreatorProfile;
        }

        private int GetUserIdFromContext()
        {
            if (HttpContext.Items["userId"] is not string userId || !int.TryParse(userId, out var id))
            {
                throw new UnauthorizedAccessException("Invalid or missing user ID.");
            }
            return id;
        }

        
        [HttpPost("create-profile")]
        public async Task<IActionResult> CreateProfile([FromBody] ContentCreatorDto contentCreatorDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            contentCreatorDto.UserId = GetUserIdFromContext();

            try
            {
                var contentCreator = await _contentCreatorProfile.AddContentCreatorProfile(contentCreatorDto);
                return Ok(contentCreator);
            }
            catch (ValidationException ex)
            {
                return StatusCode(400, ex.Message);
            }
            catch(UnauthorizedAccessException ex)
            {
                return StatusCode(403, ex.Message);
            }
        }

    }
}
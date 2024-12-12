using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        private int GetUserIdFromContext()
        {
            if (HttpContext.Items["userId"] is not string userId || !int.TryParse(userId, out var id))
            {
                throw new UnauthorizedAccessException("Invalid or missing user ID.");
            }
            return id;
        }
    }
}
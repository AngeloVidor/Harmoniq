using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Harmoniq.BLL.DTOs;
using Harmoniq.BLL.Interfaces.Albums;
using Microsoft.AspNetCore.Mvc;

namespace Harmoniq.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlbumCreatorController : ControllerBase
    {
        private readonly IAlbumCreatorService _albumCreatorService;

        public AlbumCreatorController(IAlbumCreatorService albumCreatorService)
        {
            _albumCreatorService = albumCreatorService;
        }

        private string GetContentCreatorIdFromClaims()
        {
            var contentCreatorIdClaim = User.Claims.FirstOrDefault(c => c.Type == "ContentCreatorId");

            if (contentCreatorIdClaim == null)
            {
                throw new UnauthorizedAccessException("ContentCreatorId não encontrado nas claims do usuário.");
            }

            return contentCreatorIdClaim.Value;
        }


        [HttpPost("add-album")]
        public async Task<IActionResult> AddAlbum([FromBody] AlbumDto albumDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var contentCreatorIdString = GetContentCreatorIdFromClaims();

            if (!int.TryParse(contentCreatorIdString, out int contentCreatorId))
            {
                return Unauthorized("Invalid ContentCreatorId in claims.");
            }
            albumDto.ContentCreatorId = contentCreatorId;

            try
            {
                var addedAlbum = await _albumCreatorService.AddAlbumAsync(albumDto);
                return Ok(addedAlbum);
            }
            catch(ValidationException ex)
            {
                return StatusCode(400, ex.Message);
            }
        }
    }
}
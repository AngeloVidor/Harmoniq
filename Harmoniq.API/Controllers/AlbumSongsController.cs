using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Harmoniq.BLL.DTOs;
using Harmoniq.BLL.Interfaces.AlbumSongs;
using Microsoft.AspNetCore.Mvc;

namespace Harmoniq.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlbumSongsController : ControllerBase
    {
        private readonly IAlbumSongsService _albumSongsService;

        public AlbumSongsController(IAlbumSongsService albumSongsService)
        {
            _albumSongsService = albumSongsService;
        }

        [HttpPost("add-song-to-album")]
        public async Task<IActionResult> AddSongToAlbum([FromBody] AlbumSongsDto albumSongsDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var contentCreatorIdString = GetContentCreatorIdFromClaims();

            if (!int.TryParse(contentCreatorIdString, out int contentCreatorId))
            {
                return Unauthorized("ContentCreatorId inválido nas claims.");
            }
            albumSongsDto.ContentCreatorId = contentCreatorId;

            try
            {
                var addedAlbumSongs = await _albumSongsService.AddSongsToAlbumAsync(albumSongsDto);
                return Ok(addedAlbumSongs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
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
    }
}
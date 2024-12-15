using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Harmoniq.BLL.DTOs;
using Harmoniq.BLL.Interfaces.AlbumSongs;
using Harmoniq.BLL.Interfaces.UserContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Harmoniq.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "ContentCreator")]
    public class AlbumSongsController : ControllerBase
    {
        private readonly IAlbumSongsService _albumSongsService;
        private readonly IUserContextService _userContextService;

        public AlbumSongsController(IAlbumSongsService albumSongsService, IUserContextService userContextService)
        {
            _albumSongsService = albumSongsService;
            _userContextService = userContextService;
        }


        [HttpPost("add-song-to-album")]
        public async Task<IActionResult> AddSongToAlbum([FromBody] AlbumSongsDto albumSongsDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var contentCreator = _userContextService.GetUserIdFromContext();
            albumSongsDto.ContentCreatorId = contentCreator;

            try
            {
                var addedAlbumSongs = await _albumSongsService.AddSongsToAlbumAsync(albumSongsDto);
                return Ok(addedAlbumSongs);
            }
            catch (ValidationException ex)
            {
                return StatusCode(400, ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return StatusCode(404, ex.Message);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Harmoniq.BLL.DTOs;
using Harmoniq.BLL.Interfaces.AlbumSongs;
using Harmoniq.BLL.Interfaces.UserContext;
using Harmoniq.BLL.Interfaces.UserManagement;
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

        public AlbumSongsController
        (IAlbumSongsService albumSongsService,
        IUserContextService userContextService)
        {
            _albumSongsService = albumSongsService;
            _userContextService = userContextService;
        }


        [HttpPost("add-song-to-album")]
        public async Task<IActionResult> AddSongToAlbum([FromForm] AlbumSongsDto albumSongsDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userId = _userContextService.GetUserIdFromContext();
            var contentCreatorId = await _userContextService.GetContentCreatorIdByUserIdAsync(userId);
            albumSongsDto.ContentCreatorId = contentCreatorId;

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

        [HttpPut("edit-album-songs")]
        public async Task<IActionResult> EditAlbumSongs([FromForm] EditedAlbumSongsDto editedSongs)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userId = _userContextService.GetUserIdFromContext();
            editedSongs.ContentCreatorId = await _userContextService.GetContentCreatorIdByUserIdAsync(userId);

            try
            {
                var editedAlbumSongs = await _albumSongsService.EditAlbumSongsAsync(editedSongs);
                return Ok(editedAlbumSongs);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


    }
}
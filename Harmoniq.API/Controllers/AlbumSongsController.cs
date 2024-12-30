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
        private readonly IUserAccountService _userAccountService;

        public AlbumSongsController(IAlbumSongsService albumSongsService, IUserContextService userContextService, IUserAccountService userAccountService)
        {
            _albumSongsService = albumSongsService;
            _userContextService = userContextService;
            _userAccountService = userAccountService;
        }


        [HttpPost("add-song-to-album")]
        public async Task<IActionResult> AddSongToAlbum([FromBody] AlbumSongsDto albumSongsDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userId = _userContextService.GetUserIdFromContext();
            var contentCreatorId = await _userAccountService.GetContentCreatorIdByUserIdAsync(userId);
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
    }
}
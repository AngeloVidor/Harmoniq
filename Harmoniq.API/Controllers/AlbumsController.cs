using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Harmoniq.BLL.DTOs;
using Harmoniq.BLL.Interfaces.AlbumManagement;
using Harmoniq.BLL.Interfaces.Albums;
using Harmoniq.BLL.Interfaces.DisplayAlbums;
using Harmoniq.BLL.Interfaces.UserContext;
using Harmoniq.BLL.Interfaces.UserManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Harmoniq.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlbumsController : ControllerBase
    {
        private readonly IAlbumCreatorService _albumCreatorService;
        private readonly IUserContextService _userContextService;
        public readonly IAlbumManagementService _albumManagementService;
        private readonly IDisplayAlbumsService _displayAlbums;

        public AlbumsController(
        IAlbumCreatorService albumCreatorService,
        IUserContextService userContextService,
        IAlbumManagementService albumManagementService
,
        IDisplayAlbumsService displayAlbums
        )
        {
            _albumCreatorService = albumCreatorService;
            _userContextService = userContextService;
            _albumManagementService = albumManagementService;
            _displayAlbums = displayAlbums;
        }

        [HttpPost("album")]
        [Authorize(Roles = "ContentCreator")]
        public async Task<IActionResult> AddAlbum([FromForm] AlbumDto albumDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var contentCreator = _userContextService.GetUserIdFromContext();
            var contentCreatorId = await _userContextService.GetContentCreatorIdByUserIdAsync(contentCreator);
            albumDto.ContentCreatorId = contentCreatorId;

            try
            {
                var addedAlbum = await _albumCreatorService.AddAlbumAsync(albumDto);
                return Ok(addedAlbum);
            }
            catch (ValidationException ex)
            {
                return StatusCode(400, ex.Message);
            }
        }

        [HttpGet("albums")]
        [Authorize(Roles = "ContentCreator, ContentConsumer")]
        public async Task<IActionResult> GetAlbuns()
        {
            try
            {
                var albums = await _albumManagementService.GetAlbumsAsync();
                return Ok(albums);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{albumId}")]
        [Authorize(Roles = "ContentCreator, ContentConsumer")]
        public async Task<IActionResult> GetAlbumById([FromRoute] int albumId)
        {
            try
            {
                var album = await _albumManagementService.GetAlbumByIdAsync(albumId);
                return Ok(album);
            }
            catch (KeyNotFoundException ex)
            {
                return StatusCode(404, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("delete-album/{albumId}")]
        [Authorize(Roles = "ContentCreator")]
        public async Task<IActionResult> RemoveAlbum(int albumId)
        {
            try
            {
                var album = await _albumManagementService.RemoveAlbumAsync(albumId);
                album.IsDeleted = true;
                return Ok(album);
            }
            catch (KeyNotFoundException ex)
            {
                return StatusCode(404, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPut("album")]
        [Authorize(Roles = "ContentCreator")]
        public async Task<IActionResult> EditAlbum([FromForm] EditedAlbumDto editedAlbum)
        {

            var contentCreator = _userContextService.GetUserIdFromContext();
            var contentCreatorId = await _userContextService.GetContentCreatorIdByUserIdAsync(contentCreator);
            editedAlbum.ContentCreatorId = contentCreatorId;

            try
            {
                var editedAlbumDto = await _albumManagementService.EditAlbumAsync(editedAlbum);
                return Ok(editedAlbumDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("albums/{contentCreatorId}")]
        [Authorize(Roles = "ContentCreator, ContentConsumer")]
        public async Task<IActionResult> GetContentCreatorAlbumsAsync()
        {
            try
            {
                var userId = _userContextService.GetUserIdFromContext();
                var creatorId = await _userContextService.GetContentCreatorIdByUserIdAsync(userId);
                var albums = await _displayAlbums.GetContentCreatorAlbumsAsync(creatorId);
                return Ok(albums);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }

}
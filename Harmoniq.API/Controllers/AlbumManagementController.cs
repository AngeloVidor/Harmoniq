using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Harmoniq.BLL.DTOs;
using Harmoniq.BLL.Interfaces.AlbumManagement;
using Harmoniq.BLL.Interfaces.Albums;
using Harmoniq.BLL.Interfaces.UserContext;
using Harmoniq.BLL.Interfaces.UserManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Harmoniq.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "ContentCreator")]
    public class AlbumManagementController : ControllerBase
    {
        private readonly IAlbumCreatorService _albumCreatorService;
        private readonly IUserContextService _userContextService;
        public readonly IAlbumManagementService _albumManagementService;
        private readonly IUserAccountService _userAccountService;

        public AlbumManagementController(IAlbumCreatorService albumCreatorService, IUserContextService userContextService, IAlbumManagementService albumManagementService, IUserAccountService userAccountService)
        {
            _albumCreatorService = albumCreatorService;
            _userContextService = userContextService;
            _albumManagementService = albumManagementService;
            _userAccountService = userAccountService;
        }

        [HttpPost("add-album")]
        public async Task<IActionResult> AddAlbum([FromForm] AlbumDto albumDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var contentCreator = _userContextService.GetUserIdFromContext();
            var contentCreatorId = await _userAccountService.GetContentCreatorIdByUserIdAsync(contentCreator);
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

        [HttpGet("get-albums")]
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

        [HttpGet("get-album-by-id")]
        public async Task<IActionResult> GetAlbumsById(int albumId)
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

        [HttpPost("remove-album")]
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

    }
}
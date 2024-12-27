using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Harmoniq.BLL.DTOs;
using Harmoniq.BLL.Interfaces.AlbumManagement;
using Harmoniq.BLL.Interfaces.Albums;
using Harmoniq.BLL.Interfaces.UserContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Harmoniq.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "ContentCreator")]
    public class AlbumCreatorController : ControllerBase
    {
        private readonly IAlbumCreatorService _albumCreatorService;
        private readonly IUserContextService _userContextService;
        public readonly IAlbumManagementService _albumManagementService;

        public AlbumCreatorController(IAlbumCreatorService albumCreatorService, IUserContextService userContextService, IAlbumManagementService albumManagementService)
        {
            _albumCreatorService = albumCreatorService;
            _userContextService = userContextService;
            _albumManagementService = albumManagementService;
        }

        [HttpPost("add-album")]
        public async Task<IActionResult> AddAlbum([FromForm] AlbumDto albumDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var contentCreator = _userContextService.GetUserIdFromContext();
            albumDto.ContentCreatorId = contentCreator;

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
    }
}
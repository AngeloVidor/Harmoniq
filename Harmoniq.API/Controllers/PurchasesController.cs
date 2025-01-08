using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Harmoniq.BLL.Interfaces.AlbumManagement;
using Harmoniq.BLL.Interfaces.Discography;
using Harmoniq.BLL.Interfaces.UserContext;
using Harmoniq.BLL.Interfaces.UserManagement;
using Microsoft.AspNetCore.Mvc;

namespace Harmoniq.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PurchasesController : ControllerBase
    {
        private readonly IAlbumManagementService _albumManagementService;
        private readonly IUserAccountService _userAccountService;
        private readonly IUserContextService _userContextService;
        private readonly IDiscographyService _discographyService;


        public PurchasesController(IAlbumManagementService albumManagementService, IUserAccountService userAccountService, IUserContextService userContextService, IDiscographyService discographyService)
        {
            _albumManagementService = albumManagementService;
            _userAccountService = userAccountService;
            _userContextService = userContextService;
            _discographyService = discographyService;
        }

        [HttpGet("{consumerId}")]
        public async Task<IActionResult> GetPurchasedAlbumsByConsumerId()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                int userId = _userContextService.GetUserIdFromContext();
                var contentConsumerId = await _userContextService.GetContentConsumerIdByUserIdAsync(userId);
                var consumerId = contentConsumerId ?? 0;

                var purchasedAlbums = await _albumManagementService.GetPurchasedAlbumsByConsumerIdAsync(consumerId);
                return Ok(purchasedAlbums);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("download-discography/{albumId}")]
        public async Task<IActionResult> DownloadDiscography(int albumId, int contentConsumerId)
        {
            try
            {
                int userId = _userContextService.GetUserIdFromContext();
                contentConsumerId = await _userContextService.GetContentConsumerIdByUserIdAsync(userId) ?? -1;

                var album = await _discographyService.DownloadAlbumAsync(albumId, contentConsumerId);

                var json = System.Text.Json.JsonSerializer.Serialize(album);
                var memoryStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(json));

                return File(memoryStream, "application/json", $"album-{albumId}.json");
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
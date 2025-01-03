using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Harmoniq.BLL.Interfaces.Discography;
using Harmoniq.BLL.Interfaces.UserContext;
using Microsoft.AspNetCore.Mvc;

namespace Harmoniq.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DiscographyController : ControllerBase
    {
        private readonly IDiscographyService _discographyService;
        private readonly IUserContextService _userContextService;

        public DiscographyController(IDiscographyService discographyService, IUserContextService userContextService)
        {
            _discographyService = discographyService;
            _userContextService = userContextService;
        }

        [HttpGet("download/{albumId}")]
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
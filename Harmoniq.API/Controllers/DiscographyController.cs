using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Harmoniq.BLL.Interfaces.Discography;
using Microsoft.AspNetCore.Mvc;

namespace Harmoniq.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DiscographyController : ControllerBase
    {
        private readonly IDiscographyService _discographyService;

        public DiscographyController(IDiscographyService discographyService)
        {
            _discographyService = discographyService;
        }

        [HttpGet("download/{albumId}")]
        public async Task<IActionResult> DownloadDiscography(int albumId)
        {
            try
            {
                var album = await _discographyService.DownloadAlbumAsync(albumId);

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
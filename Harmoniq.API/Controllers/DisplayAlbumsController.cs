using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Harmoniq.BLL.Interfaces.DisplayAlbums;
using Microsoft.AspNetCore.Mvc;

namespace Harmoniq.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DisplayAlbumsController : ControllerBase
    {
        private readonly IDisplayAlbumsService _displayAlbums;

        public DisplayAlbumsController(IDisplayAlbumsService displayAlbums)
        {
            _displayAlbums = displayAlbums;
        }


        [HttpGet("content-creator-albums/{contentCreatorId}")]
        public async Task<IActionResult> GetContentCreatorAlbumsAsync(int contentCreatorId)
        {
            try
            {
                var albums = await _displayAlbums.GetContentCreatorAlbumsAsync(contentCreatorId);
                return Ok(albums);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
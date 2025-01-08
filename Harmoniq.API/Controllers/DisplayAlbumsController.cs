using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Harmoniq.BLL.Interfaces.DisplayAlbums;
using Harmoniq.BLL.Interfaces.UserContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Harmoniq.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DisplayAlbumsController : ControllerBase
    {
        private readonly IDisplayAlbumsService _displayAlbums;
        private readonly IUserContextService _userContext;

        public DisplayAlbumsController(IDisplayAlbumsService displayAlbums, IUserContextService userContext)
        {
            _displayAlbums = displayAlbums;
            _userContext = userContext;
        }


        [HttpGet("{contentCreatorId}")]
        public async Task<IActionResult> GetContentCreatorAlbumsAsync(int contentCreatorId)
        {
            try
            {
                var userId = _userContext.GetUserIdFromContext();
                var creatorId = await _userContext.GetContentCreatorIdByUserIdAsync(userId);
                var albums = await _displayAlbums.GetContentCreatorAlbumsAsync(creatorId);
                return Ok(albums);
            }
            catch(KeyNotFoundException ex)
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
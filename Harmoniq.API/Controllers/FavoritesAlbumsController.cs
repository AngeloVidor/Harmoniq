using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Harmoniq.BLL.DTOs;
using Harmoniq.BLL.Interfaces.Favorites;
using Harmoniq.BLL.Interfaces.UserContext;
using Microsoft.AspNetCore.Mvc;

namespace Harmoniq.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FavoritesAlbumsController : ControllerBase
    {
        private readonly IFavoritesAlbumsService _favoritesAlbums;
        private readonly IUserContextService _userContext;

        public FavoritesAlbumsController(IFavoritesAlbumsService favoritesAlbums, IUserContextService userContext)
        {
            _favoritesAlbums = favoritesAlbums;
            _userContext = userContext;
        }


        [HttpPost("favorite-album")]
        public async Task<IActionResult> FavoriteAlbum([FromBody] FavoritesAlbumsDto favorite)
        {
            if (favorite == null)
            {
                return BadRequest();
            }

            var userId = _userContext.GetUserIdFromContext();
            favorite.ContentConsumerId = await _userContext.GetContentConsumerIdByUserIdAsync(userId) ?? -1;

            try
            {
                var favoritedAlbum = await _favoritesAlbums.AddFavoriteAlbumAsync(favorite);
                return Ok(favoritedAlbum);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
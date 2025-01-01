using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Harmoniq.BLL.DTOs;
using Harmoniq.BLL.Interfaces.UserContext;
using Harmoniq.BLL.Interfaces.Wishlist;
using Microsoft.AspNetCore.Mvc;

namespace Harmoniq.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WishlistController : ControllerBase
    {
        private readonly IWishlistService _wishlistService;
        private readonly IUserContextService _userContextService;

        public WishlistController(IWishlistService wishlistService, IUserContextService userContextService)
        {
            _wishlistService = wishlistService;
            _userContextService = userContextService;
        }

        [HttpPost("add-album-to-wishlist")]
        public async Task<IActionResult> AddAlbumToWishlist([FromBody] WishlistDto wishlist)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = _userContextService.GetUserIdFromContext();
            wishlist.ContentConsumerId = await _userContextService.GetContentConsumerIdByUserIdAsync(userId) ?? -1;

            try
            {
                var result = await _wishlistService.AddAlbumToWishlist(wishlist);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
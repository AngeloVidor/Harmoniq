using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Harmoniq.BLL.DTOs;
using Harmoniq.BLL.Interfaces.UserContext;
using Harmoniq.BLL.Interfaces.Wishlist;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Harmoniq.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "ContentConsumer")]

    public class WishlistController : ControllerBase
    {
        private readonly IWishlistService _wishlistService;
        private readonly IUserContextService _userContextService;

        public WishlistController(IWishlistService wishlistService, IUserContextService userContextService)
        {
            _wishlistService = wishlistService;
            _userContextService = userContextService;
        }

        [HttpPost("albums")]
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

        [HttpGet("/api/wishlist/{consumerId}")]
        public async Task<IActionResult> GetWishlistByContentConsumerId()
        {
            var userId = _userContextService.GetUserIdFromContext();
            var contentConsumerId = await _userContextService.GetContentConsumerIdByUserIdAsync(userId) ?? -1;

            try
            {
                var userWishlist = await _wishlistService.GetWishlistByContentConsumerId(contentConsumerId);
                return Ok(userWishlist);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("wishlist/{wishlistId}/album/{albumId}")]
        public async Task<IActionResult> DeleteAlbumFromWishlist(int albumId)
        {
            var userId = _userContextService.GetUserIdFromContext();
            var consumerId = (int)await _userContextService.GetContentConsumerIdByUserIdAsync(userId);
            var wishlistId = await _userContextService.GetWishlistIdByConsumerIdAsync(consumerId);
            try
            {
                var response = await _wishlistService.DeleteAlbumFromWishlist(wishlistId, albumId, consumerId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
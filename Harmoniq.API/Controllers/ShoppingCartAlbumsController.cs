using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Harmoniq.BLL.DTOs;
using Harmoniq.BLL.Interfaces.CartAlbums;
using Harmoniq.BLL.Interfaces.UserContext;
using Microsoft.AspNetCore.Mvc;

namespace Harmoniq.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShoppingCartAlbumsController : ControllerBase
    {
        private readonly ICartAlbumsService _cartAlbums;
        private readonly IUserContextService _userContext;

        public ShoppingCartAlbumsController(ICartAlbumsService cartAlbums, IUserContextService userContext)
        {
            _cartAlbums = cartAlbums;
            _userContext = userContext;
        }

        [HttpPost("add-album-to-cart")]
        public async Task<IActionResult> AddAlbumToCart([FromBody] CartAlbumDto cart)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = _userContext.GetUserIdFromContext();
            var consumer = (int)await _userContext.GetContentConsumerIdByUserIdAsync(userId);
            cart.CartId = await _cartAlbums.GetCartIdByContentConsumerIdAsync(consumer);

            try
            {
                var addedToCart = await _cartAlbums.AddAlbumToCartAsync(cart);
                return Ok(addedToCart);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
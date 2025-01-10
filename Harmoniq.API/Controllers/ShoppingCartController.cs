using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Harmoniq.BLL.DTOs;
using Harmoniq.BLL.Interfaces.Cart;
using Harmoniq.BLL.Interfaces.CartAlbums;
using Harmoniq.BLL.Interfaces.UserContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Harmoniq.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "ContentConsumer")]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IUserContextService _userContextService;
        private readonly ICartAlbumsService _cartAlbums;


        public ShoppingCartController(IShoppingCartService shoppingCartService, IUserContextService userContextService, ICartAlbumsService cartAlbums)
        {
            _shoppingCartService = shoppingCartService;
            _userContextService = userContextService;
            _cartAlbums = cartAlbums;
        }

        [HttpPost("cart")]
        public async Task<IActionResult> AddNewShoppingCart([FromBody] CartDto cart)
        {
            if (cart == null)
            {
                return BadRequest();
            }

            var userId = _userContextService.GetUserIdFromContext();
            var consumerId = cart.ContentConsumerId = await _userContextService.GetContentConsumerIdByUserIdAsync(userId) ?? -1;

            var consumerCart = await _shoppingCartService.GetCartByConsumerIdAsync(consumerId);
            if (consumerCart != null && consumerCart.IsCheckedOut == true)
            {
                var newCart = new CartDto
                {
                    ContentConsumerId = consumerId,
                    IsCheckedOut = false
                };

                var createdCart = await _shoppingCartService.AddNewShoppingCart(newCart);
                return Ok(createdCart);

            }

            try
            {
                var newCart = await _shoppingCartService.AddNewShoppingCart(cart);
                return Ok(newCart);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("album")]
        public async Task<IActionResult> AddAlbumToCart([FromBody] CartAlbumDto cart)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = _userContextService.GetUserIdFromContext();
            var consumerId = (int)await _userContextService.GetContentConsumerIdByUserIdAsync(userId);


            var consumerCart = await _shoppingCartService.GetCartByConsumerIdAsync(consumerId);
            if (consumerCart.IsCheckedOut)
            {
                var activeCart = await _shoppingCartService.GetActiveCartIdByConsumerIdAsync(consumerId);
                cart.CartId = activeCart.CartId;
            }

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
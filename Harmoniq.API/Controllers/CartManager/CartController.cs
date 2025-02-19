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
    public class CartController : ControllerBase
    {
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IUserContextService _userContextService;
        private readonly ICartAlbumsService _cartAlbums;


        public CartController(IShoppingCartService shoppingCartService, IUserContextService userContextService, ICartAlbumsService cartAlbums)
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
            Console.WriteLine($"Current CartId: {consumerCart.CartId}, IsCheckedOut: {consumerCart.IsCheckedOut}");

            if (consumerCart.IsCheckedOut)
            {
                var activeCart = await _shoppingCartService.GetActiveCartIdByConsumerIdAsync(consumerId);

                if (activeCart == null)
                {
                    System.Console.WriteLine("No active cart found for consumer");
                    return BadRequest("No active cart found for consumer.");
                }

                Console.WriteLine($"Using Active CartId: {activeCart.CartId}");
                cart.CartId = activeCart.CartId;
            }
            else
            {
                cart.CartId = consumerCart.CartId;
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

        [HttpPut("albums-in-cart")]
        public async Task<IActionResult> EditCartAlbums([FromRoute] EditCartAlbumDto editCartAlbumDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userId = _userContextService.GetUserIdFromContext();
            var consumerId = (int)await _userContextService.GetContentConsumerIdByUserIdAsync(userId);
            var consumerCart = await _shoppingCartService.GetCartByConsumerIdAsync(consumerId);

            var activeConsumerCart = await _shoppingCartService.GetActiveCartIdByConsumerIdAsync(consumerId);
            if (consumerCart.IsCheckedOut)
            {
                editCartAlbumDto.CartId = activeConsumerCart.CartId;
            }

            //"cartId": 3013,
            //"albumId": 7007
            else
            {
                editCartAlbumDto.CartId = consumerCart.CartId;
            }

            try
            {
                var editedCartAlbum = await _cartAlbums.UpdateCartAlbumAsync(editCartAlbumDto);
                return Ok(editedCartAlbum);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("remove-album-from-cart")]
        public async Task<IActionResult> RemoveAlbumFromCart([FromBody] CartAlbumDto cartAlbum)
        {
            var userId = _userContextService.GetUserIdFromContext();
            var consumerId = (int)await _userContextService.GetContentConsumerIdByUserIdAsync(userId);

            var consumerCart = await _shoppingCartService.GetCartByConsumerIdAsync(consumerId);
            Console.WriteLine($"Current CartId: {consumerCart.CartId}, IsCheckedOut: {consumerCart.IsCheckedOut}");

            if (consumerCart.IsCheckedOut)
            {
                var activeCart = await _shoppingCartService.GetActiveCartIdByConsumerIdAsync(consumerId);

                if (activeCart == null)
                {
                    System.Console.WriteLine("No active cart found for consumer");
                    return BadRequest("No active cart found for consumer.");
                }

                Console.WriteLine($"Using Active CartId: {activeCart.CartId}");
                cartAlbum.CartId = activeCart.CartId;
            }
            else
            {
                cartAlbum.CartId = consumerCart.CartId;
            }

            try
            {
                var deletedAlbum = await _cartAlbums.DeleteAlbumFromCartAsync(cartAlbum);
                return Ok(deletedAlbum);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("ConsumerCart/{consumerId}")]
        public async Task<IActionResult> ActiveConsumerCart([FromRoute] int consumerId)
        {
            try
            {
                var userId = _userContextService.GetUserIdFromContext();
                consumerId = (int)await _userContextService.GetContentConsumerIdByUserIdAsync(userId);
                var cart = await _shoppingCartService.GetActiveCartIdByConsumerIdAsync(consumerId);
                return Ok(cart);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Harmoniq.BLL.DTOs;
using Harmoniq.BLL.Interfaces.Cart;
using Harmoniq.BLL.Interfaces.CartAlbums;
using Harmoniq.BLL.Interfaces.Stripe;
using Harmoniq.BLL.Interfaces.UserContext;
using Harmoniq.DAL.Interfaces.CartAlbums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Harmoniq.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "ContentConsumer")]

    public class CartCheckoutSessionController : ControllerBase
    {

        private readonly ICartCheckoutSessionService _cartCheckoutSessionService;
        private readonly IUserContextService _userContextService;
        private readonly ICartAlbumsService _cartAlbumsService;
        private readonly IShoppingCartService _shoppingCartService;

        public CartCheckoutSessionController(ICartCheckoutSessionService cartCheckoutSessionService, IUserContextService userContextService, ICartAlbumsService cartAlbumsService, IShoppingCartService shoppingCartService)
        {
            _cartCheckoutSessionService = cartCheckoutSessionService;
            _userContextService = userContextService;
            _cartAlbumsService = cartAlbumsService;
            _shoppingCartService = shoppingCartService;
        }


        [HttpPost("create-checkout-session")]
        public async Task<IActionResult> CreateCheckoutSession([FromBody] CartCheckoutDto cart)
        {
            if (cart == null)
            {
                return BadRequest("Invalid cart details.");
            }

            var userId = _userContextService.GetUserIdFromContext();
            var consumerId = (int)await _userContextService.GetContentConsumerIdByUserIdAsync(userId);


            // Check if the cart is already marked as checked out
            // and switch to a new cart if one is available.
            var consumerCart = await _shoppingCartService.GetCartByConsumerIdAsync(consumerId);
            if (consumerCart.IsCheckedOut)
            {
                var activeCart = await _shoppingCartService.GetActiveCartIdByConsumerIdAsync(consumerId);
                cart.CartId = activeCart.CartId;
            }
            else
            {
                cart.CartId = consumerCart.CartId;
            }


            System.Console.WriteLine($"CartID: {cart.CartId}");
            cart.ContentConsumerId = consumerId;

            var albumsInCart = await _cartAlbumsService.GetCartAlbumsByCartIdAsync(cart.CartId);
            cart.Albums = albumsInCart.ToList();

            if (cart.Albums == null || cart.Albums.Count == 0)
            {
                return BadRequest("No albums in the cart.");
            }

            try
            {
                var session_id = await _cartCheckoutSessionService.CreateCartCheckoutSessionAsync(cart);
                return Ok(new { SessionId = session_id });
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

        [HttpGet("success")]
        public IActionResult PaymentSuccess(string session_id)
        {
            if (string.IsNullOrEmpty(session_id))
            {
                return BadRequest("Session ID is required.");
            }

            return Ok(new { Message = "Payment successful!", SessionId = session_id });
        }

        [HttpGet("cancel")]
        public IActionResult PaymentCancelled()
        {
            return Ok(new { Message = "Payment cancelled by the user." });
        }
    }
}
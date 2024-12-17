using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Harmoniq.BLL.DTOs;
using Harmoniq.BLL.Interfaces.PurchasedAlbums;
using Harmoniq.BLL.Interfaces.PurchasedAlbums;
using Harmoniq.BLL.Interfaces.RoleChecker;
using Harmoniq.BLL.Interfaces.Stripe;
using Harmoniq.BLL.Interfaces.UserContext;
using Microsoft.AspNetCore.Mvc;

namespace Harmoniq.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BuyAlbumController : ControllerBase
    {
        private readonly IBuyAlbumService _buyAlbumService;
        private readonly IUserRoleCheckerService _userRoleCheckerService;
        private readonly IUserContextService _userContextService;
        private readonly ICheckoutSessionService _checkoutSessionService;


        public BuyAlbumController(IBuyAlbumService buyAlbumService, IUserRoleCheckerService userRoleCheckerService, IUserContextService userContextService, ICheckoutSessionService checkoutSessionService)
        {
            _buyAlbumService = buyAlbumService;
            _userRoleCheckerService = userRoleCheckerService;
            _userContextService = userContextService;
            _checkoutSessionService = checkoutSessionService;
        }

        [HttpPost("buy-album")]
        public async Task<IActionResult> BuyAlbum([FromBody] PurchasedAlbumDto purchasedAlbum)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            purchasedAlbum.ContentConsumerId = _userContextService.GetContentConsumerIdFromContext();

            try
            {
                var buyedAlbum = await _buyAlbumService.BuyAlbumAsync(purchasedAlbum);
                return Ok(buyedAlbum);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }


        [HttpPost("create-checkout-session")]
        public async Task<IActionResult> CreateCheckoutSession([FromBody] CheckoutAlbumDto albumDto)
        {
            if (string.IsNullOrEmpty(albumDto.AlbumId))
            {
                return BadRequest("Invalid album details.");
            }

            if (!int.TryParse(albumDto.AlbumId, out var albumId))
            {
                return BadRequest("Invalid album ID.");
            }

            try
            {
                var album = await _buyAlbumService.GetAlbumAsync(albumId);

                if (album == null)
                {
                    return NotFound("Album not found.");
                }

                albumDto.Title = album.Title;
                albumDto.Price = album.Price;
                Console.WriteLine($"AlbumDTO: {albumDto.Title}, AlbumID: {albumDto.AlbumId}, AlbumPrice: {albumDto.Price}");


                var sessionUrl = await _checkoutSessionService.CreateCheckoutSession(albumDto.AlbumId, albumDto.Title, albumDto.Price);

                return Ok(new { Url = sessionUrl });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("cancel")]
        public IActionResult PaymentCancelled()
        {
            return Ok(new { Message = "Payment cancelled by the user." });
        }




        [HttpGet("success")]
        public IActionResult PaymentSuccess(string session_id)
        {
            if (string.IsNullOrEmpty(session_id))
            {
                return BadRequest("Session ID is required.");
            }

            // Lógica adicional, como verificar a sessão na Stripe, pode ser adicionada aqui
            return Ok(new { Message = "Payment successful!", SessionId = session_id });
        }


    }
}
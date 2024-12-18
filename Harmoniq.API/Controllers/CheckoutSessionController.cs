using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Harmoniq.BLL.DTOs;
using Harmoniq.BLL.Interfaces.AlbumManagement;
using Harmoniq.BLL.Interfaces.PurchasedAlbums;
using Harmoniq.BLL.Interfaces.Stripe;
using Microsoft.AspNetCore.Mvc;

namespace Harmoniq.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CheckoutSessionController : ControllerBase
    {

        private readonly ICheckoutSessionService _checkoutSessionService;
        private readonly IAlbumManagementService _albumManagementService;

        public CheckoutSessionController(ICheckoutSessionService checkoutSessionService, IAlbumManagementService albumManagementService)
        {
            _checkoutSessionService = checkoutSessionService;
            _albumManagementService = albumManagementService;
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
                var album = await _albumManagementService.GetAlbumAsync(albumId);

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
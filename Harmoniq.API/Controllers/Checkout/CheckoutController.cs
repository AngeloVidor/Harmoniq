using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Harmoniq.BLL.DTOs;
using Harmoniq.BLL.Interfaces.AlbumManagement;
using Harmoniq.BLL.Interfaces.PurchasedAlbums;
using Harmoniq.BLL.Interfaces.Stripe;
using Harmoniq.BLL.Interfaces.UserContext;
using Harmoniq.BLL.Interfaces.UserManagement;
using Harmoniq.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Harmoniq.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "ContentConsumer")]
    public class CheckoutController : ControllerBase
    {

        private readonly ICheckoutSessionService _checkoutSessionService;
        private readonly IAlbumManagementService _albumManagementService;
        private readonly IUserAuthService _userAuthService;
        private readonly IUserContextService _userContextService;

        public CheckoutController(ICheckoutSessionService checkoutSessionService, 
        IAlbumManagementService albumManagementService, 
        IUserAuthService userAuthService, 
        IUserContextService userContextService)
        {
            _checkoutSessionService = checkoutSessionService;
            _albumManagementService = albumManagementService;
            _userAuthService = userAuthService;
            _userContextService = userContextService;
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

            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out var userId))
            {
                return BadRequest("Invalid user ID.");
            }

            var user = await _userAuthService.GetUserByIdAsync(userId);
            if (user.Roles != AccountType.ContentConsumer)
            {
                return Unauthorized("Only ContentConsumers can make album purchases.");
            }

            try
            {
                var contentConsumerId = await _userContextService.GetContentConsumerIdByUserIdAsync(userId);
                if (contentConsumerId == null)
                {
                    return NotFound("ContentConsumerID not found.");
                }

                var album = await _albumManagementService.GetAlbumByIdAsync(albumId);

                if (album == null)
                {
                    return NotFound("Album not found.");
                }

                albumDto.Title = album.Title;
                albumDto.Price = album.Price;
                Console.WriteLine($"AlbumDTO: {albumDto.Title}, AlbumID: {albumDto.AlbumId}, AlbumPrice: {albumDto.Price}");


                var sessionUrl = await _checkoutSessionService.CreateCheckoutSession(albumDto.AlbumId, albumDto.Title, albumDto.Price, contentConsumerId.ToString());

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

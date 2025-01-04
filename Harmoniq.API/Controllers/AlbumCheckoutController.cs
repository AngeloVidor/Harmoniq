using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Harmoniq.BLL.DTOs;
using Harmoniq.BLL.Interfaces.PurchasedAlbums;
using Harmoniq.BLL.Interfaces.RoleChecker;
using Harmoniq.BLL.Interfaces.Stripe;
using Harmoniq.BLL.Interfaces.UserContext;
using Microsoft.AspNetCore.Mvc;

namespace Harmoniq.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlbumCheckoutController : ControllerBase
    {
        private readonly IAlbumCheckoutService _albumCheckout;
        private readonly IUserContextService _userContextService;


        public AlbumCheckoutController(IAlbumCheckoutService albumCheckout, IUserContextService userContextService)
        {
            _albumCheckout = albumCheckout;
            _userContextService = userContextService;
        }


        [HttpPost("buy-album")]
        public async Task<IActionResult> BuyAlbum([FromBody] PurchasedAlbumDto purchasedAlbum)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = _userContextService.GetUserIdFromContext();
            var contentConsumerId = await _userContextService.GetContentConsumerIdByUserIdAsync(userId);
            purchasedAlbum.ContentConsumerId = contentConsumerId ?? 0;

            try
            {
                var buyedAlbum = await _albumCheckout.BuyAlbumAsync(purchasedAlbum);
                return Ok(buyedAlbum);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

    }
}
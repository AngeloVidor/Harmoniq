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
        private readonly IUserContextService _userContextService;


        public BuyAlbumController(IBuyAlbumService buyAlbumService, IUserContextService userContextService)
        {
            _buyAlbumService = buyAlbumService;
            _userContextService = userContextService;
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

    }
}
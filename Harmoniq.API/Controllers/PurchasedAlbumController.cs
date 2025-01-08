using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Harmoniq.BLL.Interfaces.AlbumManagement;
using Harmoniq.BLL.Interfaces.UserContext;
using Harmoniq.BLL.Interfaces.UserManagement;
using Microsoft.AspNetCore.Mvc;

namespace Harmoniq.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PurchasedAlbumController : ControllerBase
    {
        private readonly IAlbumManagementService _albumManagementService;
        private readonly IUserAccountService _userAccountService;
        private readonly IUserContextService _userContextService;

        public PurchasedAlbumController(IAlbumManagementService albumManagementService, IUserAccountService userAccountService, IUserContextService userContextService)
        {
            _albumManagementService = albumManagementService;
            _userAccountService = userAccountService;
            _userContextService = userContextService;
        }

        [HttpGet("{consumerId}")]
        public async Task<IActionResult> GetPurchasedAlbumsByConsumerId()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                int userId = _userContextService.GetUserIdFromContext();
                var contentConsumerId = await _userContextService.GetContentConsumerIdByUserIdAsync(userId);
                var consumerId = contentConsumerId ?? 0;

                var purchasedAlbums = await _albumManagementService.GetPurchasedAlbumsByConsumerIdAsync(consumerId);
                return Ok(purchasedAlbums);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
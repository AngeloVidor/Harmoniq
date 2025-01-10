using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Harmoniq.BLL.Interfaces.Cart;
using Harmoniq.BLL.Interfaces.UserContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Harmoniq.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "ContentConsumer")]
    public class ActiveConsumerCartController : ControllerBase
    {
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IUserContextService _userContextService;

        public ActiveConsumerCartController(IShoppingCartService shoppingCartService, IUserContextService userContextService)
        {
            _shoppingCartService = shoppingCartService;
            _userContextService = userContextService;
        }

        [HttpGet("GetActiveConsumerCart/{consumerId}")]
        public async Task<IActionResult> GetActiveConsumerCart([FromRoute] int consumerId)
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
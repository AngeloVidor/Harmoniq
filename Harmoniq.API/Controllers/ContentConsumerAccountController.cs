using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Harmoniq.BLL.DTOs;
using Harmoniq.BLL.Interfaces.ContentConsumerAccount;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Harmoniq.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "ContentConsumer")]
    public class ContentConsumerAccountController : ControllerBase
    {
        private readonly IContentConsumerAccountService _contentConsumerAccount;

        public ContentConsumerAccountController(IContentConsumerAccountService contentConsumerAccount)
        {
            _contentConsumerAccount = contentConsumerAccount;
        }

        [HttpPost("add-contentConsumer-account")]
        public async Task<IActionResult> AddContentConsumerAccount([FromBody] ContentConsumerDto consumerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            consumerDto.UserId = GetUserIdFromContext();

            try
            {
                var addedContentConsumer = await _contentConsumerAccount.AddContetConsumerAccountAsync(consumerDto);
                return Ok(addedContentConsumer);
            }
            catch (ValidationException ex)
            {
                return StatusCode(400, ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                return StatusCode(403, ex.Message);
            }
        }


        private int GetUserIdFromContext()
        {
            if (HttpContext.Items["userId"] is not string userId || !int.TryParse(userId, out var id))
            {
                throw new UnauthorizedAccessException("Invalid or missing user ID.");
            }
            return id;
        }
    }
}
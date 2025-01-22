using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Harmoniq.BLL.Interfaces.Emails;
using Microsoft.AspNetCore.Mvc;

namespace Harmoniq.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SendEmailController : ControllerBase
    {
        private readonly IEmailSender _emailSender;

        public SendEmailController(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        [HttpPost("send-email")]
        //TODO: Authorize adms only 
        public async Task<IActionResult> SendEmail([FromForm] string to, [FromForm] string subject, [FromForm] string body)
        {
            await _emailSender.SendEmail(to, subject, body);
            return Ok();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Harmoniq.BLL.DTOs;
using Harmoniq.BLL.Interfaces.Tokens;
using Harmoniq.BLL.Interfaces.UserContext;
using Harmoniq.BLL.Interfaces.UserManagement;
using Microsoft.AspNetCore.Mvc;

namespace Harmoniq.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserAuthService _userAuthService;
        private readonly IMapper _mapper;
        private readonly IBearerTokenManagement _bearerTokenManagement;
        private readonly IUserContextService _userContextService;

        public AuthController(IUserContextService userContextService, IBearerTokenManagement bearerTokenManagement, IMapper mapper, IUserAuthService userAuthService)
        {
            _userAuthService = userAuthService;
            _mapper = mapper;
            _bearerTokenManagement = bearerTokenManagement;
            _userContextService = userContextService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUserAccount([FromBody] UserRegisterDto userRegisterDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var registeredUser = await _userAuthService.RegisterUserAccountAsync(userRegisterDto);
                return Ok(registeredUser);
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(409, ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dto = _mapper.Map<UserRegisterDto>(userLoginDto);
            var user = await _userAuthService.ValidateUserAccountAsync(dto);
            if (user == null)
            {
                return Unauthorized("Invalid credentials");
            }

            var token = await _bearerTokenManagement.GenerateTokenAsync(user);
            return Ok(new { Token = token, User = user });
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetActiveUser()
        {
            var userId = _userContextService.GetUserIdFromContext();
            try
            {
                var activeUser = await _userAuthService.GetActiveUserAsync(userId);
                return Ok(activeUser);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
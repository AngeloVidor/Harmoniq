using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Harmoniq.BLL.DTOs;
using Harmoniq.BLL.Interfaces.Tokens;
using Harmoniq.BLL.Interfaces.UserManagement;
using Microsoft.AspNetCore.Mvc;

namespace Harmoniq.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserAccountService _userAccountService;
        private readonly IMapper _mapper;
        private readonly IBearerTokenManagement _bearerTokenManagement;

        public AuthController(IBearerTokenManagement bearerTokenManagement, IMapper mapper, IUserAccountService userAccountService)
        {
            _userAccountService = userAccountService;
            _mapper = mapper;
            _bearerTokenManagement = bearerTokenManagement;
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
                var registeredUser = await _userAccountService.RegisterUserAccountAsync(userRegisterDto);
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
            var user = await _userAccountService.ValidateUserAccountAsync(dto);
            if (user == null)
            {
                return Unauthorized("Invalid credentials");
            }

            var token = await _bearerTokenManagement.GenerateTokenAsync(user);
            return Ok(new { Token = token, User = user });
        }
    }
}
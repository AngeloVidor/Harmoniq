using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Harmoniq.BLL.DTOs;
using Harmoniq.BLL.Interfaces.Tokens;
using Harmoniq.BLL.Interfaces.UserManagement;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Harmoniq.BLL.Services.Tokens
{
    public class BearerTokenManagement : IBearerTokenManagement
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IUserAccountService _userAccountService;

        public BearerTokenManagement(IConfiguration configuration, IMapper mapper, IUserAccountService userAccountService)
        {
            _configuration = configuration;
            _mapper = mapper;
            _userAccountService = userAccountService;
        }

        public async Task<string> GenerateTokenAsync(UserRegisterDto userRegisterDto)
        {
            var contentCreatorId = await _userAccountService.GetContentCreatorIdIfExists(userRegisterDto.Id);
            if(contentCreatorId == null)
            {
                throw new InvalidOperationException("User is not a Content Creator");
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userRegisterDto.Id.ToString()),
                new Claim(ClaimTypes.Name, userRegisterDto.Username.ToString()),
                new Claim(ClaimTypes.Role, userRegisterDto.Roles.ToString()),
                new Claim("ContentCreatorId", contentCreatorId.Value.ToString())
            };

            var secretKey = _configuration["Jwt:Key"];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                            issuer: _configuration["Jwt:Issuer"],
                            audience: _configuration["Jwt:Audience"],
                            claims: claims,
                            expires: DateTime.UtcNow.AddMinutes(
                                _configuration.GetValue<int>("Jwt:DurationInMinutes")
                            ),
                            signingCredentials: creds
                        );
            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
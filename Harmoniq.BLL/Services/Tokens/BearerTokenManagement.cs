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
using Harmoniq.BLL.Interfaces.UserContext;
using Harmoniq.BLL.Interfaces.UserManagement;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Harmoniq.BLL.Services.Tokens
{
    public class BearerTokenManagement : IBearerTokenManagement
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IUserContextService _userContextService;
        private readonly IUserAuthService _userAuthService;

        public BearerTokenManagement(IConfiguration configuration, IMapper mapper, IUserContextService userContextService, IUserAuthService userAuthService)
        {
            _configuration = configuration;
            _mapper = mapper;
            _userContextService = userContextService;
            _userAuthService = userAuthService;
        }

        public async Task<string> GenerateTokenAsync(UserRegisterDto userRegisterDto)
        {
            var contentCreatorId = await _userAuthService.GetContentCreatorIdIfExists(userRegisterDto.Id);
            var contentConsumerId = await _userContextService.GetContentConsumerIdByUserIdAsync(userRegisterDto.Id);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userRegisterDto.Id.ToString()),
                new Claim(ClaimTypes.Name, userRegisterDto.Username.ToString()),
                new Claim(ClaimTypes.Role, userRegisterDto.Roles.ToString()),
            };

            if (contentConsumerId.HasValue)
            {
                claims.Add(new Claim("ContentConsumerId", contentConsumerId.Value.ToString()));
            }

            if (contentCreatorId != null)
            {
                claims.Add(new Claim("ContentCreatorId", contentCreatorId.Value.ToString()));
            }


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
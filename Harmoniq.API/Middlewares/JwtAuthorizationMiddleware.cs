using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Harmoniq.API.Middlewares
{
    public class JwtAuthorizationMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtAuthorizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            List<string> PublicRoutes = new List<string>
            {
                "/api/Auth/register",
                "/api/Auth/login",
                "/api/checkoutsession/success",
                "/api/checkoutsession/cancel",
                "/api/stripewebhook/hook",
                "/api/stripewebhook/cart"
            };



            if (PublicRoutes.Contains(context.Request.Path.Value, StringComparer.OrdinalIgnoreCase))
            {
                await _next(context);
                return;
            }

            var tokenAccess = context
                .Request.Headers["Authorization"]
                .FirstOrDefault()
                ?.Split(" ")[1];
            if (string.IsNullOrEmpty(tokenAccess))
            {
                throw new Exception("Token to provided");
            }

            var isValidToken = ValidateToken(tokenAccess);
            if (!isValidToken)
            {
                throw new Exception("Invalid token");
            }
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadToken(tokenAccess) as JwtSecurityToken;
            var userIdClaims = jwtToken?.Claims.FirstOrDefault(c =>
                c.Type == ClaimTypes.NameIdentifier
            );

            if (userIdClaims == null)
            {
                throw new Exception("User ID claim not found");
            }

            context.Items["userId"] = userIdClaims.Value;
            await _next(context);
        }

        public bool ValidateToken(string token)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();

                var key = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes("G8p$7mZ4vQ9@hN1fTqK$3gR9fM#8xA6s")
                );
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = "http://localhost:5029",
                    ValidAudience = "http://localhost:5029",
                    IssuerSigningKey = key,
                    ClockSkew = TimeSpan.Zero
                };

                var principal = handler.ValidateToken(
                    token,
                    tokenValidationParameters,
                    out var validatedToken
                );

                if (!(validatedToken is JwtSecurityToken jwtSecurityToken))
                {
                    throw new Exception("Invalid token");
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Token validation failed: {ex.Message}");
            }
        }
    }
}
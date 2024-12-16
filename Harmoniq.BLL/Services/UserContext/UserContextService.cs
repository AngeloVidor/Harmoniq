using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Harmoniq.BLL.Interfaces.UserContext;
using Microsoft.AspNetCore.Http;

namespace Harmoniq.BLL.Services.UserContext
{
    public class UserContextService : IUserContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContextService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int GetUserIdFromContext()
        {
            var userId = _httpContextAccessor.HttpContext?.Items["userId"] as string;

            if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out var id))
            {
                throw new UnauthorizedAccessException("Invalid or missing user ID.");
            }

            return id;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Harmoniq.BLL.Interfaces.UserContext;
using Harmoniq.DAL.Interfaces.UserManagement;
using Microsoft.AspNetCore.Http;

namespace Harmoniq.BLL.Services.UserContext
{
    public class UserContextService : IUserContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserAccountRepository _userAccountRepository;

        public UserContextService(IHttpContextAccessor httpContextAccessor, IUserAccountRepository userAccountRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _userAccountRepository = userAccountRepository;
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


        public async Task<int?> GetContentConsumerIdByUserIdAsync(int userId)
        {
            return await _userAccountRepository.GetContentConsumerIdByUserIdAsync(userId);
        }

        public async Task<int> GetContentCreatorIdByUserIdAsync(int userId)
        {
            int contentCreator = await _userAccountRepository.GetContentCreatorIdByUserIdAsync(userId);
            return contentCreator;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Harmoniq.BLL.Interfaces.UserContext;
using Harmoniq.DAL.Interfaces.UserManagement;
using Harmoniq.DAL.Interfaces.Wishlist;
using Microsoft.AspNetCore.Http;

namespace Harmoniq.BLL.Services.UserContext
{
    public class UserContextService : IUserContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserAccountRepository _userAccountRepository;
        private readonly IWishlistRepository _wishlistRepository;

        public UserContextService(IHttpContextAccessor httpContextAccessor, IUserAccountRepository userAccountRepository, IWishlistRepository wishlistRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _userAccountRepository = userAccountRepository;
            _wishlistRepository = wishlistRepository;
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

        public async Task<int> GetWishlistIdByConsumerIdAsync(int consumerId)
        {
            return await _wishlistRepository.GetWishlistIdByConsumerIdAsync(consumerId);
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Harmoniq.DAL.Context;
using Harmoniq.DAL.Interfaces.AlbumManagement;
using Harmoniq.DAL.Interfaces.UserManagement;
using Harmoniq.DAL.Interfaces.Wishlist;
using Harmoniq.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Harmoniq.DAL.Repositories.Wishlist
{
    public class WishlistRepository : IWishlistRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IUserAccountRepository _userAccount;
        private readonly IAlbumManagementRepository _albumManagement;

        public WishlistRepository(ApplicationDbContext dbContext, IUserAccountRepository userAccount, IAlbumManagementRepository albumManagement)
        {
            _dbContext = dbContext;
            _userAccount = userAccount;
            _albumManagement = albumManagement;
        }

        public async Task<WishlistEntity> AddAlbumToWishlist(WishlistEntity wishlist)
        {
            var album = await _albumManagement.GetAlbumByIdAsync(wishlist.AlbumId);
            wishlist.AlbumTitle = album.Title;

            var consumer = await _userAccount.GetContentConsumerByIdAsync(wishlist.ContentConsumerId);
            wishlist.ConsumerUsername = consumer.Nickname;

            await _dbContext.Wishlist.AddAsync(wishlist);
            await _dbContext.SaveChangesAsync();
            return wishlist;
        }

        public async Task<List<WishlistEntity>> GetWishlistByContentConsumerId(int contentConsumerId)
        {
            return await _dbContext.Wishlist.Where(w => w.ContentConsumerId == contentConsumerId).ToListAsync();
        }
    }
}
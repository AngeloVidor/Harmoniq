using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Harmoniq.DAL.Context;
using Harmoniq.DAL.Interfaces.Wishlist;
using Harmoniq.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Harmoniq.DAL.Repositories.Wishlist
{
    public class WishlistRepository : IWishlistRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public WishlistRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<WishlistEntity> AddAlbumToWishlist(WishlistEntity wishlist)
        {
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
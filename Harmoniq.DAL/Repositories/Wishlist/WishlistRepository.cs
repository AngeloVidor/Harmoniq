using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Harmoniq.DAL.Context;
using Harmoniq.DAL.Interfaces.Wishlist;
using Harmoniq.Domain.Entities;

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
    }
}
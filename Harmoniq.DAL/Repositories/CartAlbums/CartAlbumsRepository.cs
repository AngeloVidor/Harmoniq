using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Harmoniq.DAL.Context;
using Harmoniq.DAL.Interfaces.CartAlbums;
using Harmoniq.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Harmoniq.DAL.Repositories.CartAlbums
{
    public class CartAlbumsRepository : ICartAlbumsRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CartAlbumsRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CartAlbumEntity> AddAlbumToCartAsync(CartAlbumEntity cartAlbumEntity)
        {
            await _dbContext.CartAlbums.AddAsync(cartAlbumEntity);
            await _dbContext.SaveChangesAsync();
            return cartAlbumEntity;
        }

        public async Task<List<CartAlbumEntity>> GetCartAlbumsByCartIdAsync(int cartId)
        {
            return await _dbContext.CartAlbums.Where(c => c.CartId == cartId).ToListAsync();
        }

        public async Task<int> GetCartIdByContentConsumerIdAsync(int contentConsumerId)
        {
            var cart = await _dbContext.ShoppingCart.FirstOrDefaultAsync(c => c.ContentConsumerId == contentConsumerId);
            if (cart == null)
            {
                throw new ArgumentNullException(nameof(cart));
            }
            return cart.CartId;
        }

    
    }
}
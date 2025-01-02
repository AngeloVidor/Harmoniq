using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Harmoniq.DAL.Context;
using Harmoniq.DAL.Interfaces.Cart;
using Harmoniq.Domain.Entities;

namespace Harmoniq.DAL.Repositories.Cart
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ShoppingCartRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CartEntity> AddAlbumToCartAsync(CartEntity cart)
        {
            await _dbContext.ShoppingCart.AddAsync(cart);
            await _dbContext.SaveChangesAsync();
            return cart;
        }
    }
}
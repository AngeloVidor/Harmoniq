using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Harmoniq.DAL.Context;
using Harmoniq.DAL.Interfaces.Cart;
using Harmoniq.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Harmoniq.DAL.Repositories.Cart
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ShoppingCartRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CartEntity> AddNewShoppingCart(CartEntity cart)
        {
            await _dbContext.ShoppingCart.AddAsync(cart);
            await _dbContext.SaveChangesAsync();
            return cart;
        }

        public async Task<CartEntity> GetActiveCartIdByConsumerIdAsync(int consumerId)
        {
            return await _dbContext.ShoppingCart.FirstOrDefaultAsync(c => c.ContentConsumerId == consumerId && c.IsCheckedOut == false);
        }

        public async Task<CartEntity> GetCartByConsumerIdAsync(int consumerId)
        {
            return await _dbContext.ShoppingCart.FirstOrDefaultAsync(c => c.ContentConsumerId == consumerId);
        }

        public async Task<CartEntity> GetCheckedOutCartByConsumerId(int consumerId)
        {
            return await _dbContext.ShoppingCart.
                FirstOrDefaultAsync(c => c.ContentConsumerId == consumerId && c.IsCheckedOut);
        }

        public async Task<bool> MarkCartAsPaidAsync(int cartId, int consumerId)
        {
            var cart = await _dbContext.ShoppingCart.Where(c => c.CartId == cartId && c.ContentConsumerId == consumerId).FirstOrDefaultAsync();
            if (cart == null)
            {
                return false;
            }

            if (cart.IsCheckedOut == true)
            {
                return false;
            }
            cart.IsCheckedOut = true;
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
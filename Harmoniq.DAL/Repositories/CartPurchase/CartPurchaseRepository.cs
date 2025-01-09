using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Harmoniq.DAL.Context;
using Harmoniq.DAL.Interfaces.CartPurchase;
using Harmoniq.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Harmoniq.DAL.Repositories.CartPurchase
{
    public class CartPurchaseRepository : ICartPurchaseRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CartPurchaseRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CartCheckoutEntity> CreateCartPurchaseAsync(CartCheckoutEntity cart)
        {
            await _dbContext.CartCheckout.AddAsync(cart);
            await _dbContext.SaveChangesAsync();
            return cart;
        }

     


    }
}
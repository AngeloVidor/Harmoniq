using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Harmoniq.Domain.Entities;

namespace Harmoniq.DAL.Interfaces.CartPurchase
{
    public interface ICartPurchaseRepository
    {
        Task<CartCheckoutEntity> CreateCartPurchaseAsync(CartCheckoutEntity cart);
    }
}
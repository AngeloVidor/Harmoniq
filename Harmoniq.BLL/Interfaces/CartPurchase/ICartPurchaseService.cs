using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Harmoniq.BLL.DTOs;

namespace Harmoniq.BLL.Interfaces.CartPurchase
{
    public interface ICartPurchaseService
    {
        Task<CartCheckoutDto> CreateCartPurchaseAsync(CartCheckoutDto cart);
    }
}
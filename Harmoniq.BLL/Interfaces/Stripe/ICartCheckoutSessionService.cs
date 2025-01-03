using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Harmoniq.BLL.DTOs;

namespace Harmoniq.BLL.Interfaces.Stripe
{
    public interface ICartCheckoutSessionService
    {
        Task<string> CreateCartCheckoutSessionAsync(CartCheckoutDto cart);
    }
}
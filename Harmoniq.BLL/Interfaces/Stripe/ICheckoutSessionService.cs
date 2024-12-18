using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Harmoniq.BLL.Interfaces.Stripe
{
    public interface ICheckoutSessionService
    {
        Task<string> CreateCheckoutSession(string albumId, string albumName, decimal albumPrice, string contentConsumerId);
        
    }
}
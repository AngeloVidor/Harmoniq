using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Harmoniq.BLL.DTOs;

namespace Harmoniq.BLL.Interfaces.Cart
{
    public interface IShoppingCartService
    {
        Task<CartDto> AddNewShoppingCart(CartDto cart);
        Task<bool> MarkCartAsPaidAsync(int cartId, int consumerId);
    }
}
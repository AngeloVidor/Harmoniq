using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Harmoniq.Domain.Entities;

namespace Harmoniq.DAL.Interfaces.Cart
{
    public interface IShoppingCartRepository
    {
        Task<CartEntity> AddNewShoppingCart(CartEntity cart);
        Task<bool> MarkCartAsPaidAsync(int cartId, int consumerId);
        Task<CartEntity> GetCheckedOutCartByConsumerId(int consumerId);
        Task<CartEntity> GetCartByConsumerIdAsync(int consumerId);
        Task<CartEntity> GetActiveCartIdByConsumerIdAsync(int consumerId);

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Harmoniq.BLL.DTOs;
using Harmoniq.BLL.Interfaces.Cart;
using Harmoniq.DAL.Interfaces.Cart;
using Harmoniq.Domain.Entities;

namespace Harmoniq.BLL.Services.Cart
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IMapper _mapper;

        public ShoppingCartService(IMapper mapper, IShoppingCartRepository shoppingCartRepository)
        {
            _mapper = mapper;
            _shoppingCartRepository = shoppingCartRepository;
        }

        public async Task<CartDto> AddNewShoppingCart(CartDto cart)
        {
            if (cart == null)
            {
                throw new ArgumentNullException(nameof(cart));
            }

            var cartEntity = _mapper.Map<CartEntity>(cart);
            var addedCart = await _shoppingCartRepository.AddNewShoppingCart(cartEntity);
            return _mapper.Map<CartDto>(addedCart);
        }


        public async Task<bool> MarkCartAsPaidAsync(int cartId, int consumerId)
        {
            return await _shoppingCartRepository.MarkCartAsPaidAsync(cartId, consumerId);
        }
    }
}
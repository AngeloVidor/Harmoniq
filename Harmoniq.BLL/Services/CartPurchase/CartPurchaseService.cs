using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Harmoniq.BLL.DTOs;
using Harmoniq.BLL.Interfaces.CartPurchase;
using Harmoniq.DAL.Interfaces.CartPurchase;
using Harmoniq.Domain.Entities;

namespace Harmoniq.BLL.Services.CartPurchase
{
    public class CartPurchaseService : ICartPurchaseService
    {
        private readonly ICartPurchaseRepository _cartPurchaseRepository;
        private readonly IMapper _mapper;

        public CartPurchaseService(IMapper mapper, ICartPurchaseRepository cartPurchaseRepository)
        {
            _mapper = mapper;
            _cartPurchaseRepository = cartPurchaseRepository;
        }

        public async Task<CartCheckoutDto> CreateCartPurchaseAsync(CartCheckoutDto cart)
        {
            var cartEntity = _mapper.Map<CartCheckoutEntity>(cart);
            var result = await _cartPurchaseRepository.CreateCartPurchaseAsync(cartEntity);
            return _mapper.Map<CartCheckoutDto>(result);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Harmoniq.BLL.DTOs;
using Harmoniq.BLL.Interfaces.Stripe;
using Harmoniq.DAL.Interfaces.AlbumManagement;
using Harmoniq.DAL.Interfaces.Cart;
using Harmoniq.DAL.Interfaces.CartAlbums;
using Harmoniq.Domain.Entities;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Checkout;

namespace Harmoniq.BLL.Services.Stripe
{
    public class CartCheckoutSessionService : ICartCheckoutSessionService
    {
        private readonly StripeModel _stripeModel;
        private readonly IAlbumManagementRepository _albumManagementRepository;
        private readonly ICartAlbumsRepository _cartAlbumsRepository;
        private readonly IMapper _mapper;

        public CartCheckoutSessionService(
            IOptions<StripeModel> stripeModel,
            IAlbumManagementRepository albumManagementRepository,
            ICartAlbumsRepository cartAlbumsRepository,
            IMapper mapper)
        {
            _stripeModel = stripeModel.Value;
            StripeConfiguration.ApiKey = _stripeModel.SecretKey;
            _albumManagementRepository = albumManagementRepository;
            _cartAlbumsRepository = cartAlbumsRepository;
            _mapper = mapper;
        }
        public async Task<string> CreateCartCheckoutSessionAsync(CartCheckoutDto cart)
        {
            var albumsInCart = await _cartAlbumsRepository.GetCartAlbumsByCartIdAsync(cart.CartId);
            if (albumsInCart == null || !albumsInCart.Any())
            {
                throw new ArgumentException("No albums found in the cart.");
            }

            cart.Albums = _mapper.Map<List<CartAlbumDto>>(albumsInCart);

            var lineItems = new List<SessionLineItemOptions>();
            foreach (var album in cart.Albums)
            {
                var albumDetails = await _albumManagementRepository.GetAlbumByIdAsync(album.AlbumId);
                if (albumDetails == null)
                {
                    throw new ArgumentException($"Album with ID {album.AlbumId} not found.");
                }

                lineItems.Add(new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)(albumDetails.Price * 100),
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = albumDetails.Title,
                        }
                    },
                    Quantity = 1
                });
            }

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = lineItems,
                Mode = "payment",
                SuccessUrl = "http://localhost:5029/api/cartcheckousession/success?session_id={CHECKOUT_SESSION_ID}",
                CancelUrl = "http://localhost:5029/api/cartcheckousession/cancel",
            };

            var service = new SessionService();
            var session = await service.CreateAsync(options);

            return session.Url;

        }
    }
}
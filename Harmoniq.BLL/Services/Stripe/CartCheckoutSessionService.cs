using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Harmoniq.BLL.DTOs;
using Harmoniq.BLL.Interfaces.Stripe;
using Harmoniq.DAL.Interfaces.AlbumManagement;
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

        public CartCheckoutSessionService(
            IOptions<StripeModel> stripeModel,
            IAlbumManagementRepository albumManagementRepository
            )
        {
            _stripeModel = stripeModel.Value;
            StripeConfiguration.ApiKey = _stripeModel.SecretKey;
            _albumManagementRepository = albumManagementRepository;
        }
        public async Task<string> CreateCartCheckoutSessionAsync(CartCheckoutDto cart)
        {
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

            var albumIds = string.Join(",", cart.Albums.Select(a => a.AlbumId));
            var consumerId = cart.ContentConsumerId.ToString();
            string cartId = cart.CartId.ToString();

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = lineItems,
                Mode = "payment",
                SuccessUrl = "http://localhost:5029/api/cartcheckoutsession/success?session_id={CHECKOUT_SESSION_ID}",
                CancelUrl = "http://localhost:5029/api/cartcheckoutsession/cancel",
                Metadata = new Dictionary<string, string>
                {
                    { "albumIds", albumIds },
                    { "contentConsumerId", consumerId },
                    { "CartId", cartId}
                }
            };

            var service = new SessionService();
            var session = await service.CreateAsync(options);

            return session.Url;

        }
    }
}
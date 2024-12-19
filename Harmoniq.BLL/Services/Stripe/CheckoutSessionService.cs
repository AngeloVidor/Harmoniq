using System.Collections.Generic;
using System.Threading.Tasks;
using Harmoniq.BLL.Interfaces.Stripe;
using Stripe;
using Stripe.Checkout;
using Microsoft.Extensions.Options;
using Harmoniq.Domain.Entities;
using Harmoniq.DAL.Interfaces.PurchasedAlbums;

namespace Harmoniq.BLL.Services.Stripe
{
    public class CheckoutSessionService : ICheckoutSessionService
    {
        private readonly StripeModel _stripeModel;
        private readonly IBuyAlbumRepository _buyAlbumRepository;
        public CheckoutSessionService(IOptions<StripeModel> stripeModel, IBuyAlbumRepository buyAlbumRepository)
        {
            _stripeModel = stripeModel.Value;
            StripeConfiguration.ApiKey = _stripeModel.SecretKey;
            _buyAlbumRepository = buyAlbumRepository;
        }

        public async Task<string> CreateCheckoutSession(string albumId, string albumName, decimal albumPrice, string contentConsumerId)
        {
            if (string.IsNullOrEmpty(albumId) || string.IsNullOrEmpty(albumName) || albumPrice <= 0)
            {
                throw new ArgumentException("Invalid album details.");
            }

            int albumIdInt = int.Parse(albumId);
            int contentConsumerIdInt = int.Parse(contentConsumerId);
            var isAlbumPurchased = await _buyAlbumRepository.IsAlbumPurchasedAsync(albumIdInt, contentConsumerIdInt);
            if (isAlbumPurchased)
            {
                throw new InvalidOperationException("Album already purchased");
            }
            
            var lineItems = new List<SessionLineItemOptions>
            {
                new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = "usd",
                        UnitAmountDecimal = albumPrice * 100,
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = albumName,
                        },
                    },
                    Quantity = 1,
                }
            };

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = lineItems,
                Mode = "payment",
                SuccessUrl = "http://localhost:5029/api/checkoutsession/success?session_id={CHECKOUT_SESSION_ID}",
                CancelUrl = "http://localhost:5029/api/checkoutsession/cancel",
                Metadata = new Dictionary<string, string>
                {
                    { "albumId", albumId },
                    { "contentConsumerId", contentConsumerId }
                }
            };

            var service = new SessionService();
            var session = await service.CreateAsync(options);

            return session.Url;
        }
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using Harmoniq.BLL.Interfaces.Stripe;
using Stripe;
using Stripe.Checkout;
using Microsoft.Extensions.Options;
using Harmoniq.Domain.Entities;

namespace Harmoniq.BLL.Services.Stripe
{
    public class CheckoutSessionService : ICheckoutSessionService
    {
        private readonly StripeModel _stripeModel;
        public CheckoutSessionService(IOptions<StripeModel> stripeModel)
        {
            _stripeModel = stripeModel.Value;
            StripeConfiguration.ApiKey = _stripeModel.SecretKey;
        }

        public async Task<string> CreateCheckoutSession(string albumId, string albumName, decimal albumPrice)
        {
            if (string.IsNullOrEmpty(albumId) || string.IsNullOrEmpty(albumName) || albumPrice <= 0)
            {
                throw new ArgumentException("Invalid album details.");
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
                SuccessUrl = "http://localhost:5029/api/buyalbum/success?session_id={CHECKOUT_SESSION_ID}",
                CancelUrl = "http://localhost:5029/api/buyalbum/cancel",
                Metadata = new Dictionary<string, string>
                {
                    { "albumId", albumId }
                }
            };

            var service = new SessionService();
            var session = await service.CreateAsync(options);

            return session.Url;
        }
    }
}

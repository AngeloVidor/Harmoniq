using System.IO;
using System.Threading.Tasks;
using Harmoniq.BLL.Interfaces.PurchasedAlbums;
using Harmoniq.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.BillingPortal;
using Stripe.Checkout;
using CheckoutSession = Stripe.Checkout.Session;
using BillingPortalSession = Stripe.BillingPortal.Session;
using Harmoniq.BLL.Interfaces.AlbumManagement;
using AutoMapper;
using Harmoniq.BLL.DTOs;
using Harmoniq.BLL.Interfaces.UserManagement;
using Harmoniq.BLL.Interfaces.CartPurchase;
using Harmoniq.BLL.Interfaces.UserContext;
using Harmoniq.BLL.Interfaces.CartAlbums;
using Harmoniq.BLL.Interfaces.Cart;
using Harmoniq.BLL.Interfaces.Stats;
using Harmoniq.BLL.Interfaces.Emails;



namespace Harmoniq.API.Webhooks
{
    [ApiController]
    [Route("api/[controller]")]
    public class StripeWebhookController : ControllerBase
    {
        private readonly string _webhookSecret;
        private readonly IAlbumManagementService _albumManagementService;
        private readonly IUserAuthService _userAuthService;
        private readonly IAlbumCheckoutService _albumCheckout;
        private readonly ICartPurchaseService _cartPurchaseService;
        private readonly IUserContextService _userContextService;
        private readonly string _cartWebhookSecret;
        private readonly ICartAlbumsService _cartAlbumsService;
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IStatisticsService _statistics;
        private readonly IEmailSender _emailSender;


        public StripeWebhookController(IOptions<StripeModel> stripeOptions, IAlbumManagementService albumManagementService, IUserAuthService userAuthService, IAlbumCheckoutService albumCheckout, ICartPurchaseService cartPurchaseService, IUserContextService userContextService, ICartAlbumsService cartAlbumsService, IShoppingCartService shoppingCartService, IStatisticsService statistics, IEmailSender emailSender)
        {
            _webhookSecret = stripeOptions.Value.WebhookSecret;
            _cartWebhookSecret = stripeOptions.Value.CartWebhookSecret;
            _albumManagementService = albumManagementService;
            _userAuthService = userAuthService;
            _albumCheckout = albumCheckout;
            _cartPurchaseService = cartPurchaseService;
            _userContextService = userContextService;
            _cartAlbumsService = cartAlbumsService;
            _shoppingCartService = shoppingCartService;
            _statistics = statistics;
            _emailSender = emailSender;
        }

        [HttpPost("hook")]
        public async Task<IActionResult> HandleStripeWebhook()
        {
            Console.WriteLine("Webhook recieved");
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            try
            {
                var signatureHeader = Request.Headers["Stripe-Signature"];
                var stripeEvent = EventUtility.ConstructEvent(json, signatureHeader, _webhookSecret);

                if (stripeEvent.Type == EventTypes.CheckoutSessionCompleted)
                {
                    var session = stripeEvent.Data.Object as CheckoutSession;

                    if (!int.TryParse(session.Metadata["albumId"], out var albumId) ||
                        !int.TryParse(session.Metadata["contentConsumerId"], out var contentConsumerId))
                    {
                        Console.WriteLine("Invalid metadata in session.");
                        return BadRequest("Invalid metadata in session.");
                    }

                    var album = await _albumManagementService.GetAlbumByIdAsync(albumId);
                    if (album == null)
                    {
                        Console.WriteLine($"Album not found: {albumId}");
                        return NotFound("Album not found.");
                    }

                    var purchasedAlbum = new PurchasedAlbumDto
                    {
                        AlbumId = albumId.ToString(),
                        ContentConsumerId = contentConsumerId
                    };


                    await _albumCheckout.BuyAlbumAsync(purchasedAlbum);

                    Console.WriteLine($"Album purchase recorded: AlbumID {albumId}, ConsumerID {contentConsumerId}");
                }
                else
                {
                    Console.WriteLine($"Unhandled event type: {stripeEvent.Type}");
                }

                return Ok();
            }
            catch (StripeException e)
            {
                Console.WriteLine($"Stripe error: {e.Message}");
                return BadRequest();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error processing webhook: {e.Message}");
                return StatusCode(500);
            }
        }

        [HttpPost("cart")]
        public async Task<IActionResult> HandleStripeCartWebhook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            try
            {
                var signatureHeader = Request.Headers["Stripe-Signature"];
                var stripeEvent = EventUtility.ConstructEvent(json, signatureHeader, _cartWebhookSecret);

                var session = stripeEvent.Data.Object as CheckoutSession;

                if (stripeEvent.Type == EventTypes.CheckoutSessionCompleted)
                {
                    var albumIdsString = session.Metadata["albumIds"];
                    var albumIds = albumIdsString.Split(',').Select(int.Parse).ToList();

                    var contentConsumerId = int.Parse(session.Metadata["contentConsumerId"]);

                    int cartId = int.Parse(session.Metadata["CartId"]);

                    var albums = new List<CartAlbumDto>();
                    decimal totalPrice = 0;

                    DateTime currentDate = DateTime.Now;
                    int month = currentDate.Month;
                    int year = currentDate.Year;

                    foreach (var albumPrice in albumIds)
                    {
                        var albumId = await _albumManagementService.GetAlbumByIdAsync(albumPrice);
                        totalPrice += albumId.Price;
                    }

                    foreach (var albumId in albumIds)
                    {
                        var album = await _albumManagementService.GetAlbumByIdAsync(albumId);
                        if (album != null)
                        {
                            albums.Add(new CartAlbumDto
                            {
                                AlbumId = album.Id,
                                CartId = cartId
                            });
                            var contentCreatorId = await _albumManagementService.GetContentCreatorIdByAlbumIdAsync(albumId);

                            var allPurchases = new AllPurchasedAlbumsDto
                            {
                                ContentCreatorId = contentCreatorId,
                                AlbumId = albumId,
                                Price = album.Price
                            };
                            await _statistics.SavePaidAlbumsForStatsAsync(allPurchases);
                        }
                    }

                    var cartCheckoutDto = new CartCheckoutDto
                    {
                        Albums = albums,
                        ContentConsumerId = contentConsumerId,
                        CartId = cartId,
                        Price = totalPrice,
                        AlbumIds = albumIds
                    };

                    var paidCart = await _shoppingCartService.MarkCartAsPaidAsync(cartId, contentConsumerId);

                    foreach (var albumId in albumIds)
                    {
                        var album = await _albumManagementService.GetAlbumByIdAsync(albumId);


                        var purchasedAlbum = new PurchasedAlbumDto
                        {
                            AlbumId = albumId.ToString(),
                            ContentConsumerId = contentConsumerId,
                            Price = album.Price
                        };
                        await _albumCheckout.BuyAlbumAsync(purchasedAlbum);

                    }

                    await _cartPurchaseService.CreateCartPurchaseAsync(cartCheckoutDto);
                    Console.WriteLine($"Album purchase recorded: Albums {albums}, ConsumerID {contentConsumerId}");

                    var userEmail = await _userAuthService.GetUserEmailByConsumerIdAsync(contentConsumerId);
                    await _emailSender.SendEmail(userEmail.Email, "Albums Purchased", "You have successfully purchased albums from Harmoniq");
                }
                return Ok();

            }
            catch (StripeException e)
            {
                Console.WriteLine($"Stripe error: {e.Message}");
                return BadRequest();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error processing webhook: {e.Message}");
                return StatusCode(500);
            }

        }




    }
}

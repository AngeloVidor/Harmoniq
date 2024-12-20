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



namespace Harmoniq.API.Webhooks
{
    [ApiController]
    [Route("api/[controller]")]
    public class StripeWebhookController : ControllerBase
    {
        private readonly string _webhookSecret;
        private readonly IAlbumManagementService _albumManagementService;
        private readonly IUserAccountService _userAccountService;
        private readonly IAlbumCheckoutService _albumCheckout;

        public StripeWebhookController(IOptions<StripeModel> stripeOptions, IAlbumManagementService albumManagementService, IUserAccountService userAccountService, IAlbumCheckoutService albumCheckout)
        {
            _webhookSecret = stripeOptions.Value.WebhookSecret;
            _albumManagementService = albumManagementService;
            _userAccountService = userAccountService;
            _albumCheckout = albumCheckout;
        }

        [HttpPost("hook")]
        public async Task<IActionResult> HandleStripeWebhook()
        {
            Console.WriteLine("Webook recieved");
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



    }
}

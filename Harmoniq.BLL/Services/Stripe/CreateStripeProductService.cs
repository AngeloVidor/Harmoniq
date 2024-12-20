using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Harmoniq.BLL.DTOs;
using Harmoniq.BLL.Interfaces.Stripe;
using Harmoniq.Domain.Entities;
using Microsoft.Extensions.Options;
using Stripe;

namespace Harmoniq.BLL.Services.Stripe
{
    public class CreateStripeProductService : ICreateStripeProductService
    {
        private readonly StripeModel _model;
        private readonly ProductService _productService;
        private readonly PriceService _priceService;


        public CreateStripeProductService(IOptions<StripeModel> model, ProductService productService, PriceService priceService)
        {
            _productService = productService;
            StripeConfiguration.ApiKey = model.Value.SecretKey;
            _priceService = priceService;
        }

        public async Task<AlbumDto> AddAlbumProductAsync(AlbumDto album)
        {
            try
            {
                var productOptions = new ProductCreateOptions
                {
                    Name = album.Title,
                    Description = album.Description,
                };

                var product = await _productService.CreateAsync(productOptions);

                var priceOptions = new PriceCreateOptions
                {
                    UnitAmount = (long)(album.Price * 100),
                    Currency = "usd",
                    Product = product.Id,
                };
                var price = await _priceService.CreateAsync(priceOptions);
                return album;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error creating product in Stripe: " + ex.Message);
            }
        }
    }
}

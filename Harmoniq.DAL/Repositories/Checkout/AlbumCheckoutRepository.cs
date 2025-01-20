using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Harmoniq.DAL.Context;
using Harmoniq.DAL.Interfaces;
using Harmoniq.DAL.Interfaces.CartPurchase;
using Harmoniq.DAL.Interfaces.ContentConsumerAccount;
using Harmoniq.DAL.Interfaces.PurchasedAlbums;
using Harmoniq.DAL.Interfaces.UserManagement;
using Harmoniq.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Harmoniq.DAL.Repositories.PurchasedAlbums
{
    public class AlbumCheckoutRepository : IAlbumCheckoutRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly INewAlbumRepository _newAlbumRepository;
        private readonly ICartPurchaseRepository _cartPurchases;


        public AlbumCheckoutRepository(ApplicationDbContext dbContext, INewAlbumRepository newAlbumRepository, ICartPurchaseRepository cartPurchases)
        {
            _dbContext = dbContext;
            _newAlbumRepository = newAlbumRepository;
            _cartPurchases = cartPurchases;
        }

        public async Task<PurchasedAlbumEntity> BuyAlbumAsync(PurchasedAlbumEntity purchasedAlbum)
        {
            var album = await _newAlbumRepository.GetAlbumByIdAsync(purchasedAlbum.AlbumId);
            if (album == null)
            {
                throw new InvalidOperationException("Album not found.");
            }

            var consumer = await _dbContext.ContentConsumers
                .Where(cc => cc.Id == purchasedAlbum.ContentConsumerId)
                .FirstOrDefaultAsync();

            if (consumer == null)
            {
                throw new UnauthorizedAccessException("ContentConsumer not found.");
            }

            purchasedAlbum.AlbumTitle = album.Title;
            purchasedAlbum.Username = consumer.Nickname;

           

            await _dbContext.PurchasedAlbums.AddAsync(purchasedAlbum);
            await _dbContext.SaveChangesAsync();

            return purchasedAlbum;
        }
    }
}
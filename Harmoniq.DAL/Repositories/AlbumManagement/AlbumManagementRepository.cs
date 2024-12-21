using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Harmoniq.DAL.Context;
using Harmoniq.DAL.Interfaces.AlbumManagement;
using Harmoniq.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Harmoniq.DAL.Repositories.AlbumManagement
{
    public class AlbumManagementRepository : IAlbumManagementRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public AlbumManagementRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<AlbumEntity> GetAlbumByIdAsync(int albumId)
        {
            return await _dbContext.Albums.Where(ai => ai.Id == albumId).FirstOrDefaultAsync();
        }

        public async Task<bool> AlbumExistsAsync(int albumId)
        {
            var album = await _dbContext.Albums.Where(a => a.Id == albumId).FirstOrDefaultAsync();
            if (album == null)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> IsAlbumPurchasedAsync(int albumId, int contentConsumerId)
        {
            return await _dbContext.PurchasedAlbums
                .AnyAsync(pa => pa.AlbumId == albumId && pa.ContentConsumerId == contentConsumerId);
        }

        public async Task<List<PurchasedAlbumEntity>> GetPurchasedAlbumsByConsumerIdAsync(int contentConsumerId)
        {
            return await _dbContext.PurchasedAlbums
                .Where(pa => pa.ContentConsumerId == contentConsumerId).ToListAsync();
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        public async Task<int> GetContentCreatorIdByAlbumIdAsync(int albumId)
        {
            var album = await _dbContext.Albums.FirstOrDefaultAsync(a => a.Id == albumId);
            if (album == null)
            {
                throw new KeyNotFoundException("Album not found");
            }
            return album.ContentCreatorId;
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

        public async Task<List<AlbumSongsEntity>> GetAlbumSongsByAlbumIdAsync(int albumId)
        {
            return await _dbContext.AlbumSongs.Where(a => a.AlbumId == albumId).ToListAsync();
        }

        public async Task<List<AlbumEntity>> GetAlbumsAsync()
        {
            return await _dbContext.Albums.ToListAsync();
        }

        public async Task<AlbumEntity> RemoveAlbumAsync(int albumId)
        {
            var album = await _dbContext.Albums.Where(a => a.Id == albumId).FirstOrDefaultAsync();
            if (album == null)
            {
                throw new KeyNotFoundException("Album not found");
            }

            var deletedAlbum = await _dbContext.PurchasedAlbums.Where(a => a.AlbumId == albumId).ToListAsync();

            _dbContext.PurchasedAlbums.UpdateRange(deletedAlbum);
            _dbContext.Albums.Update(album);
            await _dbContext.SaveChangesAsync();
            return album;
        }

        public async Task<AlbumEntity> EditAlbumAsync(AlbumEntity editedAlbum)
        {
            _dbContext.Albums.Update(editedAlbum);
            await _dbContext.SaveChangesAsync();
            return editedAlbum;
        }

        public async Task<List<AlbumEntity>> GetContentCreatorAlbumsAsync(int contentCreatorId)
        {
            return await _dbContext.Albums.Where(a => a.ContentCreatorId == contentCreatorId).ToListAsync();
        }

        public async Task<ContentCreatorEntity> GetContentCreatorByAlbumIdAsync(int albumId)
        {
            var album = await _dbContext.Albums.FirstOrDefaultAsync(a => a.Id == albumId);
            if(album == null)
            {
                throw new Exception("Album not found");
            }
            var creator = await _dbContext.ContentCreators.FirstOrDefaultAsync(cc => cc.Id == album.ContentCreatorId);
            if(creator == null)
            {
                throw new Exception("Content creator not found");
            }
            return creator;
        }
    }
}
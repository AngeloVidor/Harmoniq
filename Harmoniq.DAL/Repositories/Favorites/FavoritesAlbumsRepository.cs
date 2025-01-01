using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Harmoniq.DAL.Context;
using Harmoniq.DAL.Interfaces.Favorites;
using Harmoniq.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Harmoniq.DAL.Repositories.Favorites
{
    public class FavoritesAlbumsRepository : IFavoritesAlbumsRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public FavoritesAlbumsRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<FavoritesAlbumsEntity> AddFavoriteAlbumAsync(FavoritesAlbumsEntity favorite)
        {
            await _dbContext.FavoriteAlbums.AddAsync(favorite);
            await _dbContext.SaveChangesAsync();
            return favorite;
        }

        public async Task<FavoritesAlbumsEntity> GetFavoriteAlbumAsync(int contentConsumerId, int albumId)
        {
            return await _dbContext.FavoriteAlbums.Where(f => f.ContentConsumerId == contentConsumerId && f.AlbumId == albumId).FirstOrDefaultAsync();
        }

        public async Task<List<FavoritesAlbumsEntity>> GetFavoriteAlbumByContentConsumer(int contentConsumerId)
        {
            return await _dbContext.FavoriteAlbums.Where(f => f.ContentConsumerId == contentConsumerId).ToListAsync();
        }
    }
}
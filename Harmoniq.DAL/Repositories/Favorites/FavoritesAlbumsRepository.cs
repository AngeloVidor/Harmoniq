using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Harmoniq.DAL.Context;
using Harmoniq.DAL.Interfaces.Favorites;
using Harmoniq.Domain.Entities;

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
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Harmoniq.DAL.Context;
using Harmoniq.DAL.Interfaces.AlbumManagement;
using Harmoniq.DAL.Interfaces.Favorites;
using Harmoniq.DAL.Interfaces.UserManagement;
using Harmoniq.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Harmoniq.DAL.Repositories.Favorites
{
    public class FavoritesAlbumsRepository : IFavoritesAlbumsRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IUserAccountRepository _userAccount;
        private readonly IAlbumManagementRepository _albumManagement;
        public FavoritesAlbumsRepository(ApplicationDbContext dbContext, IUserAccountRepository userAccount, IAlbumManagementRepository albumManagement)
        {
            _dbContext = dbContext;
            _userAccount = userAccount;
            _albumManagement = albumManagement;
        }

        public async Task<FavoritesAlbumsEntity> AddFavoriteAlbumAsync(FavoritesAlbumsEntity favorite)
        {
            var contentConsumer = await _userAccount.GetContentConsumerByIdAsync(favorite.ContentConsumerId);
            favorite.ConsumerUsername = contentConsumer.Nickname;
            
            var album = await _albumManagement.GetAlbumByIdAsync(favorite.AlbumId);
            favorite.AlbumTitle = album.Title;

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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Harmoniq.Domain.Entities;

namespace Harmoniq.DAL.Interfaces.Favorites
{
    public interface IFavoritesAlbumsRepository
    {
        Task<FavoritesAlbumsEntity> AddFavoriteAlbumAsync(FavoritesAlbumsEntity favorite);
        Task<FavoritesAlbumsEntity> GetFavoriteAlbumAsync(int contentConsumerId, int albumId);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Harmoniq.BLL.DTOs;

namespace Harmoniq.BLL.Interfaces.Favorites
{
    public interface IFavoritesAlbumsService
    {
        Task<FavoritesAlbumsDto> AddFavoriteAlbumAsync(FavoritesAlbumsDto favorite);
        Task<List<GetFavoritesAlbumsDto>> GetFavoriteAlbumByContentConsumer(int contentConsumerId);
    }
}
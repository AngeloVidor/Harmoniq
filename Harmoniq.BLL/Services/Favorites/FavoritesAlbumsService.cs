using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Harmoniq.BLL.DTOs;
using Harmoniq.BLL.Interfaces.Favorites;
using Harmoniq.DAL.Interfaces.Favorites;
using Harmoniq.Domain.Entities;

namespace Harmoniq.BLL.Services.Favorites
{
    public class FavoritesAlbumsService : IFavoritesAlbumsService
    {
        private readonly IFavoritesAlbumsRepository _favoritesAlbums;
        private readonly IMapper _mapper;
        public FavoritesAlbumsService(IFavoritesAlbumsRepository favoritesAlbums, IMapper mapper)
        {
            _favoritesAlbums = favoritesAlbums;
            _mapper = mapper;
        }

        public async Task<FavoritesAlbumsDto> AddFavoriteAlbumAsync(FavoritesAlbumsDto favorite)
        {
            if (favorite == null)
            {
                throw new ArgumentNullException(nameof(favorite));
            }
            var alreadyFavorited = await _favoritesAlbums.GetFavoriteAlbumAsync(favorite.ContentConsumerId, favorite.AlbumId);
            if (alreadyFavorited != null)
            {
                throw new InvalidOperationException("Album already favorited");
            }

            var favoriteEntity = _mapper.Map<FavoritesAlbumsEntity>(favorite);
            var favoritedAlbum = await _favoritesAlbums.AddFavoriteAlbumAsync(favoriteEntity);
            return _mapper.Map<FavoritesAlbumsDto>(favoritedAlbum);
        }

        public async Task<List<FavoritesAlbumsDto>> GetFavoriteAlbumByContentConsumer(int contentConsumerId)
        {
            if (contentConsumerId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(contentConsumerId));
            }
            var favoritedAlbums = await _favoritesAlbums.GetFavoriteAlbumByContentConsumer(contentConsumerId);
            return _mapper.Map<List<FavoritesAlbumsDto>>(favoritedAlbums);
        }
    }
}
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
            var favoriteEntity = _mapper.Map<FavoritesAlbumsEntity>(favorite);
            var favoritedAlbum = await _favoritesAlbums.AddFavoriteAlbumAsync(favoriteEntity);
            return _mapper.Map<FavoritesAlbumsDto>(favoritedAlbum);
        }
    }
}
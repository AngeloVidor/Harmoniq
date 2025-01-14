using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Harmoniq.BLL.DTOs;
using Harmoniq.BLL.Interfaces.Wishlist;
using Harmoniq.DAL.Interfaces.Wishlist;
using Harmoniq.Domain.Entities;

namespace Harmoniq.BLL.Services.Wishlist
{
    public class WishlistService : IWishlistService
    {
        private readonly IWishlistRepository _wishlistRepository;
        private readonly IMapper _mapper;

        public WishlistService(IWishlistRepository wishlistRepository, IMapper mapper)
        {
            _wishlistRepository = wishlistRepository;
            _mapper = mapper;
        }

        public async Task<WishlistDto> AddAlbumToWishlist(WishlistDto wishlist)
        {
            if (wishlist == null)
            {
                throw new ArgumentNullException(nameof(wishlist));
            }
            var wishlistEntity = _mapper.Map<WishlistEntity>(wishlist);
            var albumWatchlist = await _wishlistRepository.AddAlbumToWishlist(wishlistEntity);
            return _mapper.Map<WishlistDto>(albumWatchlist);
        }

        public async Task<WishlistDto> DeleteAlbumFromWishlist(int wishlistId, int albumId, int consumerId)
        {
            if (wishlistId <= 0 && albumId <= 0)
            {
                throw new KeyNotFoundException("Not found album or wishlist");
            }
            var userWishlist = await _wishlistRepository.GetWishlistByContentConsumerId(consumerId);
            var album = userWishlist.FirstOrDefault(a => a.AlbumId == albumId);


            if (album == null)
            {
                throw new KeyNotFoundException("Album not found in your wishlist");
            }


            var deleted = await _wishlistRepository.DeleteAlbumFromWishlist(wishlistId, albumId);
            return _mapper.Map<WishlistDto>(deleted);
        }

        public async Task<List<GetWishlistDto>> GetWishlistByContentConsumerId(int contentConsumerId)
        {
            if (contentConsumerId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(contentConsumerId));
            }
            var wishlist = await _wishlistRepository.GetWishlistByContentConsumerId(contentConsumerId);
            return _mapper.Map<List<GetWishlistDto>>(wishlist);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Harmoniq.BLL.DTOs;
using Harmoniq.BLL.Interfaces.CartAlbums;
using Harmoniq.DAL.Interfaces.CartAlbums;
using Harmoniq.Domain.Entities;

namespace Harmoniq.BLL.Services.CartAlbums
{
    public class CartAlbumsService : ICartAlbumsService
    {
        private readonly ICartAlbumsRepository _cartAlbums;
        private readonly IMapper _mapper;

        public CartAlbumsService(ICartAlbumsRepository cartAlbums, IMapper mapper)
        {
            _cartAlbums = cartAlbums;
            _mapper = mapper;
        }

        public async Task<CartAlbumDto> AddAlbumToCartAsync(CartAlbumDto cartAlbumDto)
        {
            if (cartAlbumDto == null)
            {
                throw new ArgumentNullException(nameof(cartAlbumDto));
            }

            var cartAlbumEntity = _mapper.Map<CartAlbumEntity>(cartAlbumDto);
            var addedToCart = await _cartAlbums.AddAlbumToCartAsync(cartAlbumEntity);
            return _mapper.Map<CartAlbumDto>(addedToCart);
        }

        public async Task<CartAlbumDto> DeleteAlbumFromCartAsync(CartAlbumDto cartAlbum)
        {
            var cartAlbumEntity = _mapper.Map<CartAlbumEntity>(cartAlbum);
            var deletedAlbum = await _cartAlbums.DeleteAlbumFromCartAsync(cartAlbumEntity);
            return _mapper.Map<CartAlbumDto>(deletedAlbum);
        }

        public async Task<List<CartAlbumDto>> GetCartAlbumsByCartIdAsync(int cartId)
        {
            var cartAlbums = await _cartAlbums.GetCartAlbumsByCartIdAsync(cartId);
            return _mapper.Map<List<CartAlbumDto>>(cartAlbums);
        }

        public async Task<int> GetCartIdByContentConsumerIdAsync(int contentConsumerId)
        {
            return await _cartAlbums.GetCartIdByContentConsumerIdAsync(contentConsumerId);
        }

        public async Task<EditCartAlbumDto> UpdateCartAlbumAsync(EditCartAlbumDto cartAlbum)
        {
            var cartAlbumEntity = _mapper.Map<CartAlbumEntity>(cartAlbum);
            var result = await _cartAlbums.UpdateCartAlbumAsync(cartAlbumEntity);
            return _mapper.Map<EditCartAlbumDto>(result);
        }
    }
}
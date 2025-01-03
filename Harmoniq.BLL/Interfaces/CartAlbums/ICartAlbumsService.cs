using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Harmoniq.BLL.DTOs;

namespace Harmoniq.BLL.Interfaces.CartAlbums
{
    public interface ICartAlbumsService
    {
        Task<CartAlbumDto> AddAlbumToCartAsync(CartAlbumDto cartAlbumDto);
        Task<int> GetCartIdByContentConsumerIdAsync(int contentConsumerId);
    }
}
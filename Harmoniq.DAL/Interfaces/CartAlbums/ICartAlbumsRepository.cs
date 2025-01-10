using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Harmoniq.Domain.Entities;

namespace Harmoniq.DAL.Interfaces.CartAlbums
{
    public interface ICartAlbumsRepository
    {
        Task<CartAlbumEntity> AddAlbumToCartAsync(CartAlbumEntity cartAlbumEntity);

        Task<int> GetCartIdByContentConsumerIdAsync(int contentConsumerId);
        Task<List<CartAlbumEntity>> GetCartAlbumsByCartIdAsync(int cartId);
        Task<CartAlbumEntity> UpdateCartAlbumAsync(CartAlbumEntity cartAlbum);
    }
}
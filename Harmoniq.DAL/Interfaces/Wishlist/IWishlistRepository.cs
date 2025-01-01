using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Harmoniq.Domain.Entities;

namespace Harmoniq.DAL.Interfaces.Wishlist
{
    public interface IWishlistRepository
    {
        Task<WishlistEntity> AddAlbumToWishlist(WishlistEntity wishlist);
        Task<List<WishlistEntity>> GetWishlistByContentConsumerId(int contentConsumerId);
    }
}
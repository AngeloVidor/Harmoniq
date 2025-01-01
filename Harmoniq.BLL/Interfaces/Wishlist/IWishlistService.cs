using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Harmoniq.BLL.DTOs;

namespace Harmoniq.BLL.Interfaces.Wishlist
{
    public interface IWishlistService
    {
        Task<WishlistDto> AddAlbumToWishlist(WishlistDto wishlist);
    }
}
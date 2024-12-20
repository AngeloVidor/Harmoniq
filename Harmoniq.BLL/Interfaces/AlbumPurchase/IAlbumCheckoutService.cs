using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Harmoniq.BLL.DTOs;

namespace Harmoniq.BLL.Interfaces.PurchasedAlbums
{
    public interface IAlbumCheckoutService
    {
        Task<PurchasedAlbumDto> BuyAlbumAsync(PurchasedAlbumDto albumDto);
    }
}
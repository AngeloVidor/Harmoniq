using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Harmoniq.BLL.DTOs;

namespace Harmoniq.BLL.Interfaces.PurchasedAlbums
{
    public interface IBuyAlbumService
    {
        Task<PurchasedAlbumDto> BuyAlbumAsync(PurchasedAlbumDto albumDto);
        Task<AlbumDto> GetAlbumAsync(int albumId);
    }
}
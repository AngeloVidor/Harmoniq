using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Harmoniq.Domain.Entities;

namespace Harmoniq.DAL.Interfaces.PurchasedAlbums
{
    public interface IBuyAlbumRepository
    {
        Task<PurchasedAlbumEntity> BuyAlbumAsync(PurchasedAlbumEntity purchasedAlbum);
    }
}
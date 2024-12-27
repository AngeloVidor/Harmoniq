using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Harmoniq.BLL.DTOs;

namespace Harmoniq.BLL.Interfaces.AlbumManagement
{
    public interface IAlbumManagementService
    {
        Task<AlbumDto> GetAlbumByIdAsync(int albumId);
        Task<List<UserOwnedAlbumsDto>> GetPurchasedAlbumsByConsumerIdAsync(int contentConsumerId);
        Task<List<AlbumDto>> GetAlbumsAsync();

    }
}

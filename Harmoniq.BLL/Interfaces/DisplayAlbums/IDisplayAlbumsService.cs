using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Harmoniq.BLL.DTOs;

namespace Harmoniq.BLL.Interfaces.DisplayAlbums
{
    public interface IDisplayAlbumsService
    {
        Task<List<AlbumDto>> GetContentCreatorAlbumsAsync(int contentConsumerId);
    }
}
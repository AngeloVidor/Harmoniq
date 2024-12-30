using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Harmoniq.BLL.DTOs;

namespace Harmoniq.BLL.Interfaces.Discography
{
    public interface IDiscographyService
    {
        Task<AlbumDto> DownloadAlbumAsync(int albumId, int contentConsumerId);

    }
}
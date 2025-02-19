using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Harmoniq.BLL.DTOs;

namespace Harmoniq.BLL.Interfaces.Albums.NewAlbum
{
    public interface INewAlbumService
    {
        Task<AlbumDto> AddAlbumAsync(AlbumDto album);
    }
}
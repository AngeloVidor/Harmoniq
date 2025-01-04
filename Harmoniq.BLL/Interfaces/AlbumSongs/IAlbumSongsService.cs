using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Harmoniq.BLL.DTOs;

namespace Harmoniq.BLL.Interfaces.AlbumSongs
{
    public interface IAlbumSongsService
    {
        Task<AlbumSongsDto> AddSongsToAlbumAsync(AlbumSongsDto albumSongsDto);
        Task<EditedAlbumSongsDto> EditAlbumSongsAsync(EditedAlbumSongsDto editedSongs);

    }
}
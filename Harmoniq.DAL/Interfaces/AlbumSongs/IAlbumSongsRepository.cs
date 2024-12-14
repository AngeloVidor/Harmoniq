using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Harmoniq.Domain.Entities;

namespace Harmoniq.DAL.Interfaces.AlbumSongs
{
    public interface IAlbumSongsRepository
    {
        Task<AlbumSongsEntity> AddSongsToAlbumAsync(AlbumSongsEntity albumSongsEntity);
        Task<bool> AlbumExistsAsync(int albumId);
    }
}
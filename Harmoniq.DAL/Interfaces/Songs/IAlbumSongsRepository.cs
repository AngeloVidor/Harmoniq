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
        Task<AlbumSongsEntity> EditAlbumSongsAsync(AlbumSongsEntity editedSongs);
        Task<AlbumSongsEntity> DeleteSongAsync(int songId, int albumId, int contentCreatorId);
        Task<List<AlbumSongsEntity>> GetContentCreatorSongsAsync(int contentCreatorId);
    }
}
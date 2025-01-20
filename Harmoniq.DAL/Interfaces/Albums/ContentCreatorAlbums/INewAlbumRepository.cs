using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Harmoniq.Domain.Entities;

namespace Harmoniq.DAL.Interfaces
{
    public interface INewAlbumRepository
    {
        Task<AlbumEntity> AddAlbumAsync(AlbumEntity album);
        Task<AlbumEntity> GetAlbumByIdAsync(int albumId);

    }
}
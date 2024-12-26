using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Harmoniq.Domain.Entities;

namespace Harmoniq.DAL.Interfaces.DisplayAlbums
{
    public interface IDisplayAlbumsRepository
    {
        Task<List<AlbumEntity>> GetContentCreatorAlbumsAsync(int contentCreatorId);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Harmoniq.Domain.Entities;

namespace Harmoniq.DAL.Interfaces.AlbumManagement
{
    public interface IAlbumManagementRepository
    {
        Task<AlbumEntity> GetAlbumAsync(int albumId);

    }
}
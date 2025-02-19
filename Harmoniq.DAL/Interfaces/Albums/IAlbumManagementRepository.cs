using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Harmoniq.Domain.Entities;

namespace Harmoniq.DAL.Interfaces.AlbumManagement
{
    public interface IAlbumManagementRepository
    {
        Task<AlbumEntity> GetAlbumByIdAsync(int albumId);
        Task<bool> AlbumExistsAsync(int albumId);
        Task<bool> IsAlbumPurchasedAsync(int albumId, int contentConsumerId);
        Task<List<PurchasedAlbumEntity>> GetPurchasedAlbumsByConsumerIdAsync(int contentConsumerId);
        Task<List<AlbumSongsEntity>> GetAlbumSongsByAlbumIdAsync(int albumId);
        Task<List<AlbumEntity>> GetAlbumsAsync();
        Task<AlbumEntity> RemoveAlbumAsync(int albumId);
        Task<AlbumEntity> EditAlbumAsync(AlbumEntity editedAlbum);
        Task<int> GetContentCreatorIdByAlbumIdAsync(int albumId);
        Task<List<AlbumEntity>> GetContentCreatorAlbumsAsync(int contentCreatorId);
        Task<ContentCreatorEntity> GetContentCreatorByAlbumIdAsync(int albumId);
    }
}
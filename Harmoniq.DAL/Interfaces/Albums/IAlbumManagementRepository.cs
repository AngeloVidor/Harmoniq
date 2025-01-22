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

//Consumer
//eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjQiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiU3BlY3RyYWwgTGFtZW50IiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiQ29udGVudENvbnN1bWVyIiwiZXhwIjoxNzM3NTcwODM1LCJpc3MiOiJodHRwOi8vbG9jYWxob3N0OjUwMjkiLCJhdWQiOiJodHRwOi8vbG9jYWxob3N0OjUwMjkifQ.PSGBCnySJl1RllbFcjoOUX_4_v_Z6GP_WflWrHlVKkA
// -> 4
//Creator
//eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjUiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoibm90cyIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IkNvbnRlbnRDcmVhdG9yIiwiQ29udGVudENyZWF0b3JJZCI6IjUiLCJleHAiOjE3Mzc1NzEwMTcsImlzcyI6Imh0dHA6Ly9sb2NhbGhvc3Q6NTAyOSIsImF1ZCI6Imh0dHA6Ly9sb2NhbGhvc3Q6NTAyOSJ9.qh1XleuKP_kBmF1GX2I91ECR-i828aGZzrqqgUkcbA0
// -> 3

    }
}
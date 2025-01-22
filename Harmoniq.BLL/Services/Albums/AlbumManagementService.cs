using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Harmoniq.BLL.DTOs;
using Harmoniq.BLL.Interfaces.AlbumManagement;
using Harmoniq.BLL.Interfaces.AWS;
using Harmoniq.DAL.Interfaces.AlbumManagement;
using Harmoniq.DAL.Interfaces.PurchasedAlbums;
using Harmoniq.Domain.Entities;

namespace Harmoniq.BLL.Services.AlbumManagement
{
    public class AlbumManagementService : IAlbumManagementService
    {
        private readonly IAlbumManagementRepository _albumManagement;
        private readonly IMapper _mapper;
        private readonly ICloudImageService _cloudImageService;

        public AlbumManagementService(IAlbumManagementRepository albumManagement, IMapper mapper, ICloudImageService cloudImageService)
        {
            _albumManagement = albumManagement;
            _mapper = mapper;
            _cloudImageService = cloudImageService;
        }

        public async Task<EditedAlbumDto> EditAlbumAsync(EditedAlbumDto editedAlbum)
        {
            if (editedAlbum == null)
            {
                throw new ArgumentNullException(nameof(editedAlbum));
            }
            var imageUrl = await _cloudImageService.UploadImageFileAsync(editedAlbum.ImageFile);
            editedAlbum.ImageUrl = imageUrl;

            var editedEntity = _mapper.Map<AlbumEntity>(editedAlbum);
            var response = await _albumManagement.EditAlbumAsync(editedEntity);
            return _mapper.Map<EditedAlbumDto>(response);
        }

        public async Task<AlbumDto> GetAlbumByIdAsync(int albumId)
        {
            var album = await _albumManagement.GetAlbumByIdAsync(albumId);
            if (album == null || album.IsDeleted == true)
            {
                throw new KeyNotFoundException("AlbumId not found. The album may have also been deleted by the artist");
            }
            return _mapper.Map<AlbumDto>(album);
        }

        public async Task<List<AlbumDto>> GetAlbumsAsync()
        {
            var albuns = await _albumManagement.GetAlbumsAsync();
            var albunsFiltered = albuns.Where(a => a.IsDeleted == false).ToList();
            return _mapper.Map<List<AlbumDto>>(albunsFiltered);
        }

        public async Task<int> GetContentCreatorIdByAlbumIdAsync(int albumId)
        {
            return await _albumManagement.GetContentCreatorIdByAlbumIdAsync(albumId);
        }

        public async Task<List<UserOwnedAlbumsDto>> GetPurchasedAlbumsByConsumerIdAsync(int contentConsumerId)
        {
            if (contentConsumerId <= 0)
            {
                throw new ArgumentException("Invalid content consumer id");
            }
            var albums = await _albumManagement.GetPurchasedAlbumsByConsumerIdAsync(contentConsumerId);

            var userOwnedAlbums = new List<UserOwnedAlbumsDto>();
            foreach (var album in albums)
            {
                var albumSongs = await _albumManagement.GetAlbumSongsByAlbumIdAsync(album.AlbumId);
                var purchasedAlbumDto = _mapper.Map<UserOwnedAlbumsDto>(album);
                purchasedAlbumDto.AlbumSongs = _mapper.Map<List<AlbumSongsDto>>(albumSongs);
                userOwnedAlbums.Add(purchasedAlbumDto);
            }
            return userOwnedAlbums;
        }

        public async Task<AlbumDto> RemoveAlbumAsync(int albumId)
        {
            if (albumId <= 0)
            {
                throw new ArgumentException("Invalid album id");
            }
            var album = await _albumManagement.GetAlbumByIdAsync(albumId);
            album.IsDeleted = true;
            if (album == null)
            {
                throw new KeyNotFoundException("AlbumId not found");
            }
            return _mapper.Map<AlbumDto>(await _albumManagement.RemoveAlbumAsync(albumId));
        }

        public async Task<List<AlbumDto>> GetContentCreatorAlbumsAsync(int contentCreatorId)
        {
            if (contentCreatorId <= 0)
            {
                throw new KeyNotFoundException("Content creator ID must be greater than 0.");
            }

            var albums = await _albumManagement.GetContentCreatorAlbumsAsync(contentCreatorId);
            if (albums == null || albums.Count == 0)
            {
                throw new KeyNotFoundException("No albums found for the specified content creator.");
            }
            return _mapper.Map<List<AlbumDto>>(albums);
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Harmoniq.BLL.DTOs;
using Harmoniq.BLL.Interfaces.Discography;
using Harmoniq.DAL.Interfaces.AlbumManagement;

namespace Harmoniq.BLL.Services.Discography
{
    public class DiscographyService : IDiscographyService
    {
        private readonly IAlbumManagementRepository _albumManagementRepository;
        private readonly IMapper _mapper;

        public DiscographyService(IAlbumManagementRepository albumManagementRepository, IMapper mapper)
        {
            _albumManagementRepository = albumManagementRepository;
            _mapper = mapper;
        }

        public async Task<AlbumDto> DownloadAlbumAsync(int albumId, int contentConsumerId)
        {
            var isPurchased = await _albumManagementRepository.IsAlbumPurchasedAsync(albumId, contentConsumerId);
            if (!isPurchased)
            {
                throw new UnauthorizedAccessException("Album not purchased.");
            }
            
            var album = await _albumManagementRepository.GetAlbumByIdAsync(albumId);
            if (album == null)
            {
                throw new KeyNotFoundException("Album not found.");
            }
            return _mapper.Map<AlbumDto>(album);
        }
    }
}
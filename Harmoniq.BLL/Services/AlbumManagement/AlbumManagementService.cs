using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Harmoniq.BLL.DTOs;
using Harmoniq.BLL.Interfaces.AlbumManagement;
using Harmoniq.DAL.Interfaces.AlbumManagement;
using Harmoniq.DAL.Interfaces.PurchasedAlbums;

namespace Harmoniq.BLL.Services.AlbumManagement
{
    public class AlbumManagementService : IAlbumManagementService
    {
        private readonly IAlbumManagementRepository _albumManagement;
        private readonly IMapper _mapper;

        public AlbumManagementService(IAlbumManagementRepository albumManagement, IMapper mapper)
        {
            _albumManagement = albumManagement;
            _mapper = mapper;
        }

        public async Task<AlbumDto> GetAlbumByIdAsync(int albumId)
        {
            var album = await _albumManagement.GetAlbumByIdAsync(albumId);
            return _mapper.Map<AlbumDto>(album);
        }

        public async Task<List<PurchasedAlbumDto>> GetPurchasedAlbumsByConsumerIdAsync(int contentConsumerId)
        {
            if (contentConsumerId <= 0)
            {
                throw new ArgumentException("Invalid content consumer id");
            }
            var albums = await _albumManagement.GetPurchasedAlbumsByConsumerIdAsync(contentConsumerId);
            var purchasedAlbums = new List<PurchasedAlbumDto>();
            foreach (var album in albums)
            {
                var purchasedAlbumDto = _mapper.Map<PurchasedAlbumDto>(album);
                purchasedAlbums.Add(purchasedAlbumDto);
            }
            return purchasedAlbums;
        }
    }
}
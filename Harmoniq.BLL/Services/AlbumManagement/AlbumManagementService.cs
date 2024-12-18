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

        public async Task<AlbumDto> GetAlbumAsync(int albumId)
        {
            var album = await _albumManagement.GetAlbumAsync(albumId);
            return _mapper.Map<AlbumDto>(album);
        }
    }
}
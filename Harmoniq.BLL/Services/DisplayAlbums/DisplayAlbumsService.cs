using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Harmoniq.BLL.DTOs;
using Harmoniq.BLL.Interfaces.DisplayAlbums;
using Harmoniq.DAL.Interfaces.DisplayAlbums;

namespace Harmoniq.BLL.Services.DisplayAlbums
{
    public class DisplayAlbumsService : IDisplayAlbumsService
    {
        private readonly IDisplayAlbumsRepository _displayAlbums;
        private readonly IMapper _mapper;
        public DisplayAlbumsService(IDisplayAlbumsRepository displayAlbums, IMapper mapper)
        {
            _displayAlbums = displayAlbums;
            _mapper = mapper;
        }

        public async Task<List<AlbumDto>> GetContentCreatorAlbumsAsync(int contentConsumerId)
        {
            var albums = await _displayAlbums.GetContentCreatorAlbumsAsync(contentConsumerId);
            return _mapper.Map<List<AlbumDto>>(albums);
        }
    }
}
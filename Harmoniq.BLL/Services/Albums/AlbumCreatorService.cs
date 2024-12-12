using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Harmoniq.BLL.DTOs;
using Harmoniq.BLL.Interfaces.Albums;
using Harmoniq.DAL.Interfaces;
using Harmoniq.Domain.Entities;

namespace Harmoniq.BLL.Services.Albums
{
    public class AlbumCreatorService : IAlbumCreatorService
    {
        private readonly IAlbumCreatorRepository _albumCreatorRepository;
        private readonly IMapper _mapper;

        public AlbumCreatorService(IAlbumCreatorRepository albumCreatorRepository, IMapper mapper)
        {
            _albumCreatorRepository = albumCreatorRepository;
            _mapper = mapper;
        }

        public async Task<AlbumDto> AddAlbumAsync(AlbumDto album)
        {
            if(album == null)
            {
                throw new ArgumentNullException(nameof(album));
            }
            var albumEntity = _mapper.Map<AlbumEntity>(album);
            var addedAlbum = await _albumCreatorRepository.AddAlbumAsync(albumEntity);
            return _mapper.Map<AlbumDto>(addedAlbum);
        }
    }
}
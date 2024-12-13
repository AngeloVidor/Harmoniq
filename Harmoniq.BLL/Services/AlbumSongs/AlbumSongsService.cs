using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Harmoniq.BLL.DTOs;
using Harmoniq.BLL.Interfaces.AlbumSongs;
using Harmoniq.DAL.Interfaces.AlbumSongs;
using Harmoniq.Domain.Entities;

namespace Harmoniq.BLL.Services.AlbumSongs
{
    public class AlbumSongsService : IAlbumSongsService
    {
        private readonly IAlbumSongsRepository _albumSongsRepository;
        private readonly IMapper _mapper;
        public AlbumSongsService(IAlbumSongsRepository albumSongsRepository, IMapper mapper)
        {
            _albumSongsRepository = albumSongsRepository;
            _mapper = mapper;
        }

        public async Task<AlbumSongsDto> AddSongsToAlbumAsync(AlbumSongsDto albumSongsDto)
        {
            if (albumSongsDto == null)
            {
                throw new ArgumentNullException("albumSongsDto cannot be null");
            }
            var albumSongEntity = _mapper.Map<AlbumSongsEntity>(albumSongsDto);
            var addedAlbumSongs = await _albumSongsRepository.AddSongsToAlbumAsync(albumSongEntity);
            return _mapper.Map<AlbumSongsDto>(addedAlbumSongs);
        }
    }
}
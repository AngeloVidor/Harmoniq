using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using Harmoniq.BLL.DTOs;
using Harmoniq.BLL.Interfaces.AlbumSongs;
using Harmoniq.BLL.Interfaces.AWS;
using Harmoniq.DAL.Interfaces.AlbumManagement;
using Harmoniq.DAL.Interfaces.AlbumSongs;
using Harmoniq.Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using ValidationException = FluentValidation.ValidationException;

namespace Harmoniq.BLL.Services.AlbumSongs
{
    public class AlbumSongsService : IAlbumSongsService
    {
        private readonly IAlbumSongsRepository _albumSongsRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<AlbumSongsDto> _validator;
        private readonly IAlbumManagementRepository _albumManagement;
        private readonly ICloudTrackService _cloudTrackService;
        public AlbumSongsService(IAlbumSongsRepository albumSongsRepository, IMapper mapper, IValidator<AlbumSongsDto> validator, IAlbumManagementRepository albumManagement, ICloudTrackService cloudTrackService)
        {
            _albumSongsRepository = albumSongsRepository;
            _mapper = mapper;
            _validator = validator;
            _albumManagement = albumManagement;
            _cloudTrackService = cloudTrackService;
        }

        public async Task<AlbumSongsDto> AddSongsToAlbumAsync(AlbumSongsDto albumSongsDto)
        {
            var validator = await _validator.ValidateAsync(albumSongsDto);
            if (!validator.IsValid)
            {
                throw new ValidationException(string.Join("; ", validator.Errors.Select(e => e.ErrorMessage)));
            }

            var albumExist = await _albumManagement.AlbumExistsAsync(albumSongsDto.AlbumId);
            if (!albumExist)
            {
                throw new KeyNotFoundException($"Album with Id: {albumSongsDto.AlbumId} not found");
            }

            var trackUrl = await _cloudTrackService.UploadAudioFileAsync(albumSongsDto.TrackFile);
            albumSongsDto.TrackUrl = trackUrl;


            var albumSongEntity = _mapper.Map<AlbumSongsEntity>(albumSongsDto);
            var addedAlbumSongs = await _albumSongsRepository.AddSongsToAlbumAsync(albumSongEntity);
            return _mapper.Map<AlbumSongsDto>(addedAlbumSongs);
        }

        public async Task<EditedAlbumSongsDto> EditAlbumSongsAsync(EditedAlbumSongsDto editedSongs)
        {
            if (editedSongs == null)
            {
                throw new ArgumentNullException(nameof(editedSongs));
            }

            var trackUrl = await _cloudTrackService.UploadAudioFileAsync(editedSongs.TrackFile);
            editedSongs.TrackUrl = trackUrl;

            var editedSongsEntity = _mapper.Map<AlbumSongsEntity>(editedSongs);
            var response = await _albumSongsRepository.EditAlbumSongsAsync(editedSongsEntity);
            return _mapper.Map<EditedAlbumSongsDto>(response);
        }
    }
}
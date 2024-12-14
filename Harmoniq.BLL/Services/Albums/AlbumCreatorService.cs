using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
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
        private readonly IValidator<AlbumDto> _validator;

        public AlbumCreatorService(IAlbumCreatorRepository albumCreatorRepository, IMapper mapper, IValidator<AlbumDto> validator)
        {
            _albumCreatorRepository = albumCreatorRepository;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<AlbumDto> AddAlbumAsync(AlbumDto album)
        {
            var validator = await _validator.ValidateAsync(album);
            if (!validator.IsValid)
            {
                throw new ValidationException(string.Join("; ", validator.Errors.Select(x => x.ErrorMessage)));
            }

            var albumEntity = _mapper.Map<AlbumEntity>(album);
            var addedAlbum = await _albumCreatorRepository.AddAlbumAsync(albumEntity);
            return _mapper.Map<AlbumDto>(addedAlbum);
        }
    }
}
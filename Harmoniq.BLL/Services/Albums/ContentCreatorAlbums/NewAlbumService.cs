using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using Harmoniq.BLL.DTOs;
using Harmoniq.BLL.Interfaces.Albums;
using Harmoniq.BLL.Interfaces.Albums.NewAlbum;
using Harmoniq.BLL.Interfaces.AWS;
using Harmoniq.BLL.Interfaces.Stripe;
using Harmoniq.DAL.Interfaces;
using Harmoniq.Domain.Entities;

namespace Harmoniq.BLL.Services.Albums
{
    public class NewAlbumService : INewAlbumService
    {
        private readonly INewAlbumRepository _albumCreatorRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<AlbumDto> _validator;
        private readonly ICreateStripeProductService _createStripeProduct;
        private readonly ICloudImageService _cloudImageService;

        public NewAlbumService(INewAlbumRepository albumCreatorRepository, IMapper mapper, IValidator<AlbumDto> validator, ICreateStripeProductService createStripeProduct, ICloudImageService cloudImageService)
        {
            _albumCreatorRepository = albumCreatorRepository;
            _mapper = mapper;
            _validator = validator;
            _createStripeProduct = createStripeProduct;
            _cloudImageService = cloudImageService;
        }

        public async Task<AlbumDto> AddAlbumAsync(AlbumDto album)
        {
            var validator = await _validator.ValidateAsync(album);
            if (!validator.IsValid)
            {
                throw new ValidationException(string.Join("; ", validator.Errors.Select(x => x.ErrorMessage)));
            }

            var imageUrl = await _cloudImageService.UploadImageFileAsync(album.ImageFile);
            album.ImageUrl = imageUrl;

            await _createStripeProduct.AddAlbumProductAsync(album);

            var albumEntity = _mapper.Map<AlbumEntity>(album);
            var addedAlbum = await _albumCreatorRepository.AddAlbumAsync(albumEntity);
            return _mapper.Map<AlbumDto>(addedAlbum);
        }
    }
}
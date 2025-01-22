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
using Harmoniq.BLL.Interfaces.Emails;
using Harmoniq.BLL.Interfaces.Stripe;
using Harmoniq.DAL.Interfaces;
using Harmoniq.DAL.Interfaces.AlbumManagement;
using Harmoniq.DAL.Interfaces.Follows;
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
        private readonly IFollowsRepository _followsRepository;
        private readonly IEmailSender _sendEmailService;
        private readonly IAlbumManagementRepository _albumManagementRepository;

        public NewAlbumService(INewAlbumRepository albumCreatorRepository, IMapper mapper, IValidator<AlbumDto> validator, ICreateStripeProductService createStripeProduct, ICloudImageService cloudImageService, IFollowsRepository followsRepository, IEmailSender sendEmailService, IAlbumManagementRepository albumManagementRepository)
        {
            _albumCreatorRepository = albumCreatorRepository;
            _mapper = mapper;
            _validator = validator;
            _createStripeProduct = createStripeProduct;
            _cloudImageService = cloudImageService;
            _followsRepository = followsRepository;
            _sendEmailService = sendEmailService;
            _albumManagementRepository = albumManagementRepository;
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
            
            var followersEmail = await _followsRepository.GetAllFollowersEmailAsync(addedAlbum.ContentCreatorId);
            if (followersEmail.Count == 0 || followersEmail == null)
            {
                throw new Exception("No followers found");
            }
            foreach (var email in followersEmail)
            {
                var contentCreator = await _albumManagementRepository.GetContentCreatorByAlbumIdAsync(addedAlbum.Id);
                if (contentCreator == null)
                {
                    throw new Exception("Content creator not found");
                }
                await _sendEmailService.SendEmail(email, "New Album", $"A new album has been added by {contentCreator.ContentCreatorName}");
            }

            return _mapper.Map<AlbumDto>(addedAlbum);
        }
    }
}
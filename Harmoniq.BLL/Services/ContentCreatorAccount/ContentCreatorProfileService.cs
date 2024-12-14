using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using Harmoniq.BLL.DTOs;
using Harmoniq.BLL.Interfaces.ContentCreatorAccount;
using Harmoniq.DAL.Interfaces.ContentCreatorAccount;
using Harmoniq.DAL.Interfaces.UserManagement;
using Harmoniq.Domain.Entities;

namespace Harmoniq.BLL.Services.ContentCreatorAccount
{
    public class ContentCreatorProfileService : IContentCreatorProfileService
    {
        private readonly IContentCreatorProfileRepository _contentCreatorProfile;
        private readonly IUserAccountRepository _userAccountRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<ContentCreatorDto> _validator;

        public ContentCreatorProfileService(IContentCreatorProfileRepository contentCreatorProfile, IMapper mapper, IUserAccountRepository userAccountRepository, IValidator<ContentCreatorDto> validator)
        {
            _contentCreatorProfile = contentCreatorProfile;
            _mapper = mapper;
            _userAccountRepository = userAccountRepository;
            _validator = validator;
        }

        public async Task<ContentCreatorDto> AddContentCreatorProfile(ContentCreatorDto contentCreatorDto)
        {
            var validator = await _validator.ValidateAsync(contentCreatorDto);
            if (!validator.IsValid)
            {
                throw new ValidationException(string.Join("; ", validator.Errors.Select(x => x.ErrorMessage)));
            }
            var user = await _userAccountRepository.GetUserAccountByIdAsync(contentCreatorDto.UserId);
            if (user.Roles != AccountType.ContentCreator)
            {
                throw new UnauthorizedAccessException("The user does not have permission to create a Content Creator profile.");
            }   

            var contentCreator = _mapper.Map<ContentCreatorEntity>(contentCreatorDto);
            var addedContentCreator = await _contentCreatorProfile.AddContentCreatorProfile(contentCreator);
            return _mapper.Map<ContentCreatorDto>(addedContentCreator);

        }
    }
}
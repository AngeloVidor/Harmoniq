using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using Harmoniq.BLL.DTOs;
using Harmoniq.BLL.Interfaces.ContentCreatorAccount;
using Harmoniq.BLL.Interfaces.RoleChecker;
using Harmoniq.DAL.Interfaces.ContentCreatorAccount;
using Harmoniq.DAL.Interfaces.UserManagement;
using Harmoniq.Domain.Entities;

namespace Harmoniq.BLL.Services.ContentCreatorAccount
{
    public class ContentCreatorProfileService : IContentCreatorProfileService
    {
        private readonly IContentCreatorProfileRepository _contentCreatorProfile;
        private readonly IUserRoleCheckerService _userRoleChecker;
        private readonly IMapper _mapper;
        private readonly IValidator<ContentCreatorDto> _validator;

        public ContentCreatorProfileService(IContentCreatorProfileRepository contentCreatorProfile, IMapper mapper, IValidator<ContentCreatorDto> validator, IUserRoleCheckerService userRoleChecker)
        {
            _contentCreatorProfile = contentCreatorProfile;
            _mapper = mapper;
            _validator = validator;
            _userRoleChecker = userRoleChecker;
        }

        public async Task<ContentCreatorDto> AddContentCreatorProfile(ContentCreatorDto contentCreatorDto)
        {
            var validator = await _validator.ValidateAsync(contentCreatorDto);
            if (!validator.IsValid)
            {
                throw new ValidationException(string.Join("; ", validator.Errors.Select(x => x.ErrorMessage)));
            }

            var userDto = new UserDto { Id = contentCreatorDto.UserId};
            await _userRoleChecker.IsContentCreator(userDto);  

            var contentCreator = _mapper.Map<ContentCreatorEntity>(contentCreatorDto);
            var addedContentCreator = await _contentCreatorProfile.AddContentCreatorProfile(contentCreator);
            return _mapper.Map<ContentCreatorDto>(addedContentCreator);

        }

        public async Task<EditContentCreatorProfileDto> EditContentCreatorProfileAsync(EditContentCreatorProfileDto editContentCreatorDto)
        {
            if(editContentCreatorDto == null)
            {
                throw new ArgumentNullException("DTO cannot be null here");
            }
            var contentCreatorEntity = _mapper.Map<ContentCreatorEntity>(editContentCreatorDto);
            var editedProfile = await _contentCreatorProfile.EditContentCreatorProfileAsync(contentCreatorEntity);
            return _mapper.Map<EditContentCreatorProfileDto>(editedProfile);
        }
    }
}
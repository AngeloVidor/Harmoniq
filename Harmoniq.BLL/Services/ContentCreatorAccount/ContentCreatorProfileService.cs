using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Harmoniq.BLL.DTOs;
using Harmoniq.BLL.Interfaces.ContentCreatorAccount;
using Harmoniq.DAL.Interfaces.ContentCreatorAccount;
using Harmoniq.DAL.Interfaces.UserManagement;
using Harmoniq.DAL.Repositories.ContentCreatorAccount;
using Harmoniq.Domain.Entities;

namespace Harmoniq.BLL.Services.ContentCreatorAccount
{
    public class ContentCreatorProfileService : IContentCreatorProfileService
    {
        private readonly IContentCreatorProfileRepository _contentCreatorProfile;
        private readonly IUserAccountRepository _userAccountRepository;
        private readonly IMapper _mapper;

        public ContentCreatorProfileService(IContentCreatorProfileRepository contentCreatorProfile, IMapper mapper, IUserAccountRepository userAccountRepository)
        {
            _contentCreatorProfile = contentCreatorProfile;
            _mapper = mapper;
            _userAccountRepository = userAccountRepository;
        }

        public async Task<ContentCreatorDto> AddContentCreatorProfile(ContentCreatorDto contentCreatorDto)
        {
            //centralizar a validações mais tarde
            var user = await _userAccountRepository.GetUserAccountByIdAsync(contentCreatorDto.UserId);
            if (user.Roles != AccountType.ContentCreator)
            {
                throw new UnauthorizedAccessException("The user does not have permission to create a Content Creator profile.");
            }

            if(contentCreatorDto.UserId <= 0)
            {
                throw new ArgumentNullException(nameof(contentCreatorDto.UserId));
            }

            var contentCreator = _mapper.Map<ContentCreatorEntity>(contentCreatorDto);
            var addedContentCreator = await _contentCreatorProfile.AddContentCreatorProfile(contentCreator);
            return _mapper.Map<ContentCreatorDto>(addedContentCreator);

        }
    }
}
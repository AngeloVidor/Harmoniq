using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using Harmoniq.BLL.DTOs;
using Harmoniq.BLL.Interfaces.ContentConsumerAccount;
using Harmoniq.BLL.Interfaces.RoleChecker;
using Harmoniq.DAL.Interfaces.ContentConsumerAccount;
using Harmoniq.DAL.Interfaces.UserManagement;
using Harmoniq.Domain.Entities;

namespace Harmoniq.BLL.Services.ContentConsumerAccount
{
    public class ContentConsumerAccountService : IContentConsumerAccountService
    {
        private readonly IContentConsumerAccountRepository _contentConsumerAccount;
        private readonly IMapper _mapper;
        private readonly IValidator<ContentConsumerDto> _validator;
        private readonly IUserRoleCheckerService _userRoleChecker;
        public ContentConsumerAccountService(IContentConsumerAccountRepository contentConsumerAccount, IMapper mapper, IValidator<ContentConsumerDto> validator, IUserRoleCheckerService userRoleChecker)
        {
            _contentConsumerAccount = contentConsumerAccount;
            _mapper = mapper;
            _validator = validator;
            _userRoleChecker = userRoleChecker;
        }

        public async Task<ContentConsumerDto> AddContetConsumerAccountAsync(ContentConsumerDto contentConsumer)
        {
            var validator = await _validator.ValidateAsync(contentConsumer);
            if (!validator.IsValid)
            {
                throw new ValidationException(string.Join("; ", validator.Errors.Select(x => x.ErrorMessage)));
            }
            var userDto = new UserDto { Id = contentConsumer.UserId };
            await _userRoleChecker.IsContentConsumer(userDto);

            var contentConsumerEntity = _mapper.Map<ContentConsumerEntity>(contentConsumer);
            var addedContentConsumer = await _contentConsumerAccount.AddContentConsumerAccountAsync(contentConsumerEntity);
            return _mapper.Map<ContentConsumerDto>(addedContentConsumer);
        }

        public async Task<ContentConsumerDto> GetContentConsumerProfileAsync(int contentConsumerId)
        {
            var profile = await _contentConsumerAccount.GetContentConsumerProfileAsync(contentConsumerId);
            return _mapper.Map<ContentConsumerDto>(profile);
        }

        public async Task<EditContentConsumerDto> UpdateContentConsumerProfileAsync(EditContentConsumerDto consumer)
        {
            var consumerEntity = _mapper.Map<ContentConsumerEntity>(consumer);
            var result = await _contentConsumerAccount.UpdateContentConsumerProfileAsync(consumerEntity);
            return _mapper.Map<EditContentConsumerDto>(result);
        }
    }
}
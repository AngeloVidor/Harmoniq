using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Harmoniq.BLL.DTOs;
using Harmoniq.BLL.Interfaces.UserManagement;
using Harmoniq.DAL.Context;
using Harmoniq.DAL.Interfaces.UserManagement;
using Harmoniq.Domain.Entities;

namespace Harmoniq.BLL.Services.UserManagement
{
    public class UserAccountService : IUserAccountService
    {
        private readonly IUserAccountRepository _userAccountRepository;
        private readonly IMapper _mapper;
        public UserAccountService(IMapper mapper, IUserAccountRepository userAccountRepository)
        {
            _userAccountRepository = userAccountRepository;
            _mapper = mapper;
        }
        public async Task<UserRegisterDto> RegisterUserAccountAsync(UserRegisterDto userRegisterDto)
        {
            if (userRegisterDto == null)
            {
                throw new ArgumentNullException("userRegisterDto cannot be null");
            }
            var userEmail = await _userAccountRepository.GetUserAccountByEmailAsync(userRegisterDto.Email);
            if (userEmail != null)
            {
                throw new Exception($"Email {userEmail} is already in use");
            }
            userRegisterDto.Password = BCrypt.Net.BCrypt.HashPassword(userRegisterDto.Password);

            var userEntity = _mapper.Map<UserEntity>(userRegisterDto);
            var addedUser = await _userAccountRepository.RegisterUserAccountAsync(userEntity);
            return _mapper.Map<UserRegisterDto>(addedUser);
        }

        public async Task<UserRegisterDto> ValidateUserAccountAsync(UserRegisterDto userRegisterDto)
        {
            var userEntity = await _userAccountRepository.GetUserAccountByEmailAsync(userRegisterDto.Email);
            if (userEntity == null || !BCrypt.Net.BCrypt.Verify(userRegisterDto.Password, userEntity.Password))
            {
                return null;
            }
            return _mapper.Map<UserRegisterDto>(userEntity);
        }


        //centralizar esse método em um serviço privado
        public async Task<int?> GetContentCreatorIdIfExists(int userId)
        {
            var user = await _userAccountRepository.GetUserAccountByIdAsync(userId);
            if(user != null && user.Roles == AccountType.ContentCreator)
            {
                return user.Id;
            }
            return null;
        }
    }
}
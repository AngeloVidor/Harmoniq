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
    public class UserAuthService : IUserAuthService
    {
        private readonly IUserAuthRepository _userAuthRepository;
        private readonly IMapper _mapper;
        public UserAuthService(IMapper mapper, IUserAuthRepository userAuthRepository)
        {
            _userAuthRepository = userAuthRepository;
            _mapper = mapper;
        }
        public async Task<UserRegisterDto> RegisterUserAccountAsync(UserRegisterDto userRegisterDto)
        {
            if (userRegisterDto == null)
            {
                throw new ArgumentNullException("userRegisterDto cannot be null");
            }
            var userEmail = await _userAuthRepository.GetUserAccountByEmailAsync(userRegisterDto.Email);
            if (userEmail != null)
            {
                throw new InvalidOperationException($"Email {userEmail} is already in use");
            }
            userRegisterDto.Password = BCrypt.Net.BCrypt.HashPassword(userRegisterDto.Password);

            var userEntity = _mapper.Map<UserEntity>(userRegisterDto);
            var addedUser = await _userAuthRepository.RegisterUserAccountAsync(userEntity);
            return _mapper.Map<UserRegisterDto>(addedUser);
        }

        public async Task<UserRegisterDto> ValidateUserAccountAsync(UserRegisterDto userRegisterDto)
        {
            var userEntity = await _userAuthRepository.GetUserAccountByEmailAsync(userRegisterDto.Email);
            if (userEntity == null || !BCrypt.Net.BCrypt.Verify(userRegisterDto.Password, userEntity.Password))
            {
                return null;
            }
            return _mapper.Map<UserRegisterDto>(userEntity);
        }


        //centralizar esse método em um serviço privado
        public async Task<int?> GetContentCreatorIdIfExists(int userId)
        {
            var user = await _userAuthRepository.GetUserAccountByIdAsync(userId);
            if (user != null && user.Roles == AccountType.ContentCreator)
            {
                return user.Id;
            }
            return null;
        }

        public async Task<UserDto> GetUserByIdAsync(int userId)
        {
            var user = await _userAuthRepository.GetUserAccountByIdAsync(userId);
            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> GetActiveUserAsync(int userId)
        {
            var user = await _userAuthRepository.GetActiveUserAsync(userId);
            var userEntity = _mapper.Map<UserEntity>(user);
            return _mapper.Map<UserDto>(userEntity);
        }
    }
}
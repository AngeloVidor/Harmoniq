using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Harmoniq.BLL.DTOs;
using Harmoniq.BLL.Interfaces.RoleChecker;
using Harmoniq.DAL.Interfaces.UserManagement;
using Harmoniq.Domain.Entities;

namespace Harmoniq.BLL.Services.RoleChecker
{
    public class UserRoleCheckerService : IUserRoleCheckerService
    {
        private readonly IUserAuthRepository _userAuthRepository;

        public UserRoleCheckerService(IUserAuthRepository userAuthRepository)
        {
            _userAuthRepository = userAuthRepository;
        }

        public async Task<bool> IsContentConsumer(UserDto userDto)
        {
            var user = await _userAuthRepository.GetUserAccountByIdAsync(userDto.Id);
            if (user.Roles != AccountType.ContentConsumer)
            {
                throw new UnauthorizedAccessException("The user does not have permission to create a Content Consumer profile.");
            }
            return true;
        }

        public async Task<bool> IsContentCreator(UserDto userDto)
        {
            var user = await _userAuthRepository.GetUserAccountByIdAsync(userDto.Id);
            if (user.Roles != AccountType.ContentCreator)
            {
                throw new UnauthorizedAccessException("The user does not have permission to create a Content Creator profile.");
            }
            return true;
        }
    }
}
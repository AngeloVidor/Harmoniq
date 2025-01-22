using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Harmoniq.BLL.DTOs;

namespace Harmoniq.BLL.Interfaces.UserManagement
{
    public interface IUserAuthService
    {
        Task<UserRegisterDto> RegisterUserAccountAsync(UserRegisterDto userRegisterDto);
        Task<UserRegisterDto> ValidateUserAccountAsync(UserRegisterDto userRegisterDto);
        Task<UserDto> GetActiveUserAsync(int userId);
        Task<int?> GetContentCreatorIdIfExists(int userId);
        Task<UserDto> GetUserByIdAsync(int userId);
        Task<UserDto> GetUserEmailByConsumerIdAsync(int consumerId);


    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Harmoniq.BLL.DTOs;

namespace Harmoniq.BLL.Interfaces.UserManagement
{
    public interface IUserAccountService
    {
        Task<UserRegisterDto> RegisterUserAccountAsync(UserRegisterDto userRegisterDto);
        Task<UserRegisterDto> ValidateUserAccountAsync(UserRegisterDto userRegisterDto);

        
        Task<int?> GetContentCreatorIdIfExists(int userId);
        Task<int?> GetContentConsumerIdByUserIdAsync(int userId);
        Task<UserDto> GetUserByIdAsync(int userId);
        Task<int> GetContentCreatorIdByUserIdAsync(int userId);
    }
}
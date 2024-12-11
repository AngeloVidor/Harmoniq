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
    }
}
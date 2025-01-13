using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Harmoniq.BLL.DTOs;

namespace Harmoniq.BLL.Interfaces.RoleChecker
{
    public interface IUserRoleCheckerService
    {
        Task<bool> IsContentConsumer(UserDto userDto);
        Task<bool> IsContentCreator(UserDto userDto);
    }
}
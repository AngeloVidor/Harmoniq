using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Harmoniq.Domain.Entities;

namespace Harmoniq.DAL.Interfaces.UserManagement
{
    public interface IUserAccountRepository
    {
        Task<UserEntity> RegisterUserAccountAsync(UserEntity userEntity);
        Task<UserEntity> GetUserAccountByEmailAsync(string email);
    }
}
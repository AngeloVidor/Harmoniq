using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Harmoniq.Domain.Entities;

namespace Harmoniq.DAL.Interfaces.UserManagement
{
    public interface IUserAuthRepository
    {
        Task<UserEntity> RegisterUserAccountAsync(UserEntity userEntity);
        Task<UserEntity> GetUserAccountByEmailAsync(string email);
        Task<UserEntity> GetUserAccountByIdAsync(int id);
        Task<int?> GetContentConsumerIdByUserIdAsync(int userId);
        Task<int> GetContentCreatorIdByUserIdAsync(int userId);
        Task<UserEntity> GetActiveUserAsync(int userId);
        Task<ContentConsumerEntity> GetContentConsumerByIdAsync(int contentConsumerId);



    }
}
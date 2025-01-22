using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Harmoniq.DAL.Context;
using Harmoniq.DAL.Interfaces.UserManagement;
using Harmoniq.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Harmoniq.DAL.Repositories.UserManagement
{
    public class UserAuthRepository : IUserAuthRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public UserAuthRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UserEntity> GetUserAccountByEmailAsync(string email)
        {
            return await _dbContext.Users.Where(em => em.Email == email).FirstOrDefaultAsync();
        }

        public async Task<UserEntity> GetUserAccountByIdAsync(int id)
        {
            return await _dbContext.Users.Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public async Task<UserEntity> RegisterUserAccountAsync(UserEntity userEntity)
        {
            await _dbContext.Users.AddAsync(userEntity);
            await _dbContext.SaveChangesAsync();
            return userEntity;
        }

        public async Task<int> GetContentCreatorIdByUserIdAsync(int userId)
        {
            var contentCreator = await _dbContext.ContentCreators
                .Where(cc => cc.UserId == userId)
                .FirstOrDefaultAsync();

            return contentCreator.Id;

        }


        public async Task<int?> GetContentConsumerIdByUserIdAsync(int userId)
        {
            var contentConsumer = await _dbContext.ContentConsumers
                .Where(cc => cc.UserId == userId)
                .FirstOrDefaultAsync();

            return contentConsumer?.Id;
        }

        public async Task<ContentConsumerEntity> GetContentConsumerByIdAsync(int contentConsumerId)
        {
            return await _dbContext.ContentConsumers.Where(cc => cc.Id == contentConsumerId).FirstOrDefaultAsync();
        }

        public async Task<UserEntity> GetActiveUserAsync(int userId)
        {
            return await _dbContext.Users.FindAsync(userId);
        }

        public async Task<UserEntity> GetUserEmailByConsumerIdAsync(int consumerId)
        {
            var user = await _dbContext.ContentConsumers
                .FirstOrDefaultAsync(cc => cc.Id == consumerId);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            return await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Id == user.UserId);
        }
    }
}
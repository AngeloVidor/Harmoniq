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
    public class UserAccountRepository : IUserAccountRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public UserAccountRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UserEntity> GetUserAccountByEmailAsync(string email)
        {
            return await _dbContext.Users.Where(em => em.Email == email).FirstOrDefaultAsync();
        }

        public async Task<UserEntity> RegisterUserAccountAsync(UserEntity userEntity)
        {
            await _dbContext.Users.AddAsync(userEntity);
            await _dbContext.SaveChangesAsync();
            return userEntity;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Harmoniq.DAL.Context;
using Harmoniq.DAL.Interfaces.ContentCreatorAccount;
using Harmoniq.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Harmoniq.DAL.Repositories.ContentCreatorAccount
{
    public class ContentCreatorProfileRepository : IContentCreatorProfileRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ContentCreatorProfileRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ContentCreatorEntity> AddContentCreatorProfile(ContentCreatorEntity contentCreatorEntity)
        {
            await _dbContext.ContentCreators.AddAsync(contentCreatorEntity);
            await _dbContext.SaveChangesAsync();
            return contentCreatorEntity;
        }

        public async Task<ContentCreatorEntity> EditContentCreatorProfileAsync(ContentCreatorEntity contentCreatorEntity)
        {
            var existingCreator = await _dbContext.ContentCreators
                                                  .FirstOrDefaultAsync(cc => cc.UserId == contentCreatorEntity.UserId);

            if (existingCreator == null)
            {
                throw new KeyNotFoundException("User not found");
            }
            _dbContext.ContentCreators.Update(existingCreator);
            await _dbContext.SaveChangesAsync();
            return contentCreatorEntity;
        }

    }
}
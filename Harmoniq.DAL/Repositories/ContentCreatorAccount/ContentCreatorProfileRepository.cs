using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Harmoniq.DAL.Context;
using Harmoniq.DAL.Interfaces.ContentCreatorAccount;
using Harmoniq.Domain.Entities;

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
    }
}
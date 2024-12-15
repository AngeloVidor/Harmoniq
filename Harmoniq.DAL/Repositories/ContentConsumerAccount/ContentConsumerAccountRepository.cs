using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Harmoniq.DAL.Context;
using Harmoniq.DAL.Interfaces.ContentConsumerAccount;
using Harmoniq.Domain.Entities;

namespace Harmoniq.DAL.Repositories.ContentConsumerAccount
{
    public class ContentConsumerAccountRepository : IContentConsumerAccountRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ContentConsumerAccountRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ContentConsumerEntity> AddContentConsumerAccountAsync(ContentConsumerEntity contentConsumer)
        {
            await _dbContext.ContentConsumers.AddAsync(contentConsumer);
            await _dbContext.SaveChangesAsync();
            return contentConsumer;
        }
    }
}
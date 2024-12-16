using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Harmoniq.Domain.Entities;

namespace Harmoniq.DAL.Interfaces.ContentConsumerAccount
{
    public interface IContentConsumerAccountRepository
    {
        Task<ContentConsumerEntity> AddContentConsumerAccountAsync(ContentConsumerEntity contentConsumer);
        Task<ContentConsumerEntity> GetContentConsumerByIdAsync(int contentConsumerId);

    }
}
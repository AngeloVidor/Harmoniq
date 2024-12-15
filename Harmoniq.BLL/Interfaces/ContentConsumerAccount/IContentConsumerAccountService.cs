using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Harmoniq.BLL.DTOs;

namespace Harmoniq.BLL.Interfaces.ContentConsumerAccount
{
    public interface IContentConsumerAccountService
    {
        Task<ContentConsumerDto> AddContetConsumerAccountAsync(ContentConsumerDto contentConsumer);
    }
}
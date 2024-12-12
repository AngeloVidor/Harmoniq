using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Harmoniq.Domain.Entities;

namespace Harmoniq.DAL.Interfaces.ContentCreatorAccount
{
    public interface IContentCreatorProfileRepository 
    {
        Task<ContentCreatorEntity> AddContentCreatorProfile(ContentCreatorEntity contentCreatorEntity);
    }
}
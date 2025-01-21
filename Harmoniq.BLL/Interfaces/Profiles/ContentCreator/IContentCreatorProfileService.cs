using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Harmoniq.BLL.DTOs;

namespace Harmoniq.BLL.Interfaces.ContentCreatorAccount
{
    public interface IContentCreatorProfileService
    {
        Task<ContentCreatorDto> AddContentCreatorProfile(ContentCreatorDto contentCreatorDto);
        Task<EditContentCreatorProfileDto> EditContentCreatorProfileAsync(EditContentCreatorProfileDto editContentCreatorDto);
        Task<ContentCreatorDto> GetContentCreatorProfileAsync(int contentCreatorId);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Harmoniq.Domain.Entities;

namespace Harmoniq.DAL.Interfaces.Follows
{
    public interface IFollowsRepository
    {
        Task<FollowersEntity> FollowAsync(FollowersEntity follow);
        Task<int> CountFollowersAsync(int contentCreatorId);
        Task<bool> IsAlreadyFollowingAsync(int followerId, int followedCreatorId);
        Task<FollowersEntity> StopFollowingAsync(int followerId, int followedCreatorId);
        Task<List<string>> GetAllFollowersEmailAsync(int creatorId);
    }
}
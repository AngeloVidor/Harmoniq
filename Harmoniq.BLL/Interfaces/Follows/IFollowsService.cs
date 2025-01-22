using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Harmoniq.BLL.DTOs;

namespace Harmoniq.BLL.Interfaces.Follows
{
    public interface IFollowsService
    {
        Task<FollowersDto> FollowAsync(FollowersDto follow);
        Task<FollowersDto> StopFollowingAsync(int followerId, int followedCreatorId);
    }
}
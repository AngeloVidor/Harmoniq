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
    }
}
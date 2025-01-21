using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Harmoniq.DAL.Context;
using Harmoniq.DAL.Interfaces.Follows;
using Harmoniq.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Harmoniq.DAL.Repositories.Follows
{
    public class FollowsRepository : IFollowsRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public FollowsRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> CountFollowersAsync(int contentCreatorId)
        {
            var followers = await _dbContext.Follows
                .Where(f => f.FollowedCreatorId == contentCreatorId)
                .ToListAsync();

            return followers.Count;
        }

        public async Task<FollowersEntity> FollowAsync(FollowersEntity follow)
        {
            await _dbContext.Follows.AddAsync(follow);
            await _dbContext.SaveChangesAsync();
            return follow;
        }

        public async Task<bool> IsAlreadyFollowingAsync(int followerId, int followedCreatorId)
        {
            return await _dbContext.Follows.AnyAsync(f => f.FollowerConsumerId == followerId && f.FollowedCreatorId == followedCreatorId);
        }

        public async Task<FollowersEntity> StopFollowingAsync(int followerId, int followedCreatorId)
        {
            var follow = await _dbContext.Follows
                .FirstOrDefaultAsync(f => f.FollowerConsumerId == followerId && f.FollowedCreatorId == followedCreatorId);

            if (follow == null)
            {
                return null;
            }

            _dbContext.Follows.Remove(follow);
            await _dbContext.SaveChangesAsync();
            return follow;
        }
    }
}
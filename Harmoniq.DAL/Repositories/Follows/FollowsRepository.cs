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
    }
}
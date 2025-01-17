using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Harmoniq.DAL.Context;
using Harmoniq.DAL.Interfaces.Stats;
using Harmoniq.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Harmoniq.DAL.Repositories.Stats
{
    public class StatisticsRepository : IStatisticsRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public StatisticsRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<StatisticsEntity> AddStatisticsAsync(StatisticsEntity statistics)
        {
            await _dbContext.Stats.AddAsync(statistics);
            await _dbContext.SaveChangesAsync();
            return statistics;
        }

        // public async Task<StatisticsEntity> GetStatisticsAsync(int year, int month, int contentCreatorId)
        // {
        //     return await _dbContext.Stats
        //         .FirstOrDefaultAsync(s => s.ContentCreatorId == contentCreatorId && s.Year == year && s.Month == month);
        // }

        public async Task<StatisticsAlbumsEntity> AddAlbumsStatisticsAsync(StatisticsAlbumsEntity albumStats)
        {
            await _dbContext.StatisticsAlbums.AddAsync(albumStats);
            await _dbContext.SaveChangesAsync();
            return albumStats;
        }

    }
}
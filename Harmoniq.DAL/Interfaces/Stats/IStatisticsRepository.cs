using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Harmoniq.Domain.Entities;

namespace Harmoniq.DAL.Interfaces.Stats
{
    public interface IStatisticsRepository
    {
        Task<StatisticsEntity> AddStatisticsAsync(StatisticsEntity statistics);
        Task<StatisticsEntity> GetStatisticsAsync(int year, int month, int contentCreatorId);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Harmoniq.BLL.DTOs;

namespace Harmoniq.BLL.Interfaces.Stats
{
    public interface IStatisticsService
    {
        Task<StatisticsDto> AddStatisticsAsync(StatisticsDto statistics);
        Task<StatisticsDto> GetStatisticsAsync(int year, int month, int contentCreatorId);
    }
}
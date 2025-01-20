using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Harmoniq.BLL.DTOs;

namespace Harmoniq.BLL.Interfaces.Stats
{
    public interface IStatisticsService
    {
        Task<FinalStatsDto> GetMonthlyStatisticsAsync(int year, int month, int contentCreatorId);
        Task<AllPurchasedAlbumsDto> SavePaidAlbumsForStatsAsync(AllPurchasedAlbumsDto albumStats);


    }
}
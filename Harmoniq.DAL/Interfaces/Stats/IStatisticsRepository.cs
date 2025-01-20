using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Harmoniq.Domain.Entities;

namespace Harmoniq.DAL.Interfaces.Stats
{
    public interface IStatisticsRepository
    {


        Task<List<AllPurchasedAlbumsEntity>> GetMonthlyStatisticsAsync(int year, int month, int contentCreatorId);
        Task<AllPurchasedAlbumsEntity> SavePaidAlbumsForStatsAsync(AllPurchasedAlbumsEntity albumStats);

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Harmoniq.BLL.DTOs;
using Harmoniq.BLL.Interfaces.Stats;
using Harmoniq.DAL.Interfaces.Stats;
using Harmoniq.Domain.Entities;

namespace Harmoniq.BLL.Services.Stats
{
    public class StatisticsService : IStatisticsService
    {
        private readonly IStatisticsRepository _statisticsRepository;
        private readonly IMapper _mapper;

        public StatisticsService(IStatisticsRepository statisticsRepository, IMapper mapper)
        {
            _statisticsRepository = statisticsRepository;
            _mapper = mapper;
        }



        public async Task<AllPurchasedAlbumsDto> SavePaidAlbumsForStatsAsync(AllPurchasedAlbumsDto albumStats)
        {
            albumStats.Month = DateTime.Now.Month;
            albumStats.Year = DateTime.Now.Year;

            var allPurchases = _mapper.Map<AllPurchasedAlbumsEntity>(albumStats);
            var response = await _statisticsRepository.SavePaidAlbumsForStatsAsync(allPurchases);
            return _mapper.Map<AllPurchasedAlbumsDto>(response);
        }

        public async Task<FinalStatsDto> GetMonthlyStatisticsAsync(int year, int month, int contentCreatorId)
        {
            var allAlbums = await _statisticsRepository.GetMonthlyStatisticsAsync(year, month, contentCreatorId);
            var finalStats = new FinalStatsDto
            {
                Year = year,
                Month = month,
                ContentCreatorId = contentCreatorId,
                TotalPrice = 0,
                Quantity = allAlbums.Count(),
                AlbumIds = new List<int>()
            };

            foreach (var album in allAlbums)
            {
                finalStats.TotalPrice += album.Price;

                if (!finalStats.AlbumIds.Contains(album.AlbumId))
                {
                    finalStats.AlbumIds.Add(album.AlbumId);
                }
            }
            return finalStats;


        }
    }
}
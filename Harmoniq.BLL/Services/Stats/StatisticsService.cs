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

        public async Task<StatisticsDto> AddStatisticsAsync(StatisticsDto statistics)
        {
            var statsEntity = _mapper.Map<StatisticsEntity>(statistics);
            var response = await _statisticsRepository.AddStatisticsAsync(statsEntity);
            return _mapper.Map<StatisticsDto>(response);
        }

        public async Task<StatisticsAlbumsDto> AddAlbumsStatisticsAsync(StatisticsAlbumsDto albumStats)
        {
            var albumStatsEntity = _mapper.Map<StatisticsAlbumsEntity>(albumStats);
            var response = await _statisticsRepository.AddAlbumsStatisticsAsync(albumStatsEntity);
            return _mapper.Map<StatisticsAlbumsDto>(response);
        }

        // public async Task<StatisticsDto> GetStatisticsAsync(int year, int month, int contentCreatorId)
        // {
        //     var stats = await _statisticsRepository.GetStatisticsAsync(year, month, contentCreatorId);
        //     if (year != stats.Year && month != stats.Month && contentCreatorId != stats.ContentCreatorId)
        //     {
        //         throw new KeyNotFoundException("Not found");
        //     }
        //     return _mapper.Map<StatisticsDto>(stats);
        // }
    }
}
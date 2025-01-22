using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Harmoniq.BLL.DTOs;
using Harmoniq.BLL.Interfaces.Follows;
using Harmoniq.DAL.Interfaces.Follows;
using Harmoniq.Domain.Entities;

namespace Harmoniq.BLL.Services.Follows
{
    public class FollowsService : IFollowsService
    {
        private readonly IFollowsRepository _followsRepository;
        private readonly IMapper _mapper;

        public FollowsService(IFollowsRepository followsRepository, IMapper mapper)
        {
            _followsRepository = followsRepository;
            _mapper = mapper;
        }

        public async Task<FollowersDto> FollowAsync(FollowersDto follow)
        {
            var alreadyFollowed = await _followsRepository.IsAlreadyFollowingAsync(follow.FollowerConsumerId, follow.FollowedCreatorId);
            if (alreadyFollowed)
            {
                throw new Exception("Already following this creator");
            }

            var followersEntity = _mapper.Map<FollowersEntity>(follow);
            var result = await _followsRepository.FollowAsync(followersEntity);
            return _mapper.Map<FollowersDto>(result);
        }


        public async Task<FollowersDto> StopFollowingAsync(int followerId, int followedCreatorId)
        {
            var unfollow = await _followsRepository.StopFollowingAsync(followerId, followedCreatorId);
            if (unfollow == null)
            {
                throw new Exception("Not following this creator");
            }
            return _mapper.Map<FollowersDto>(unfollow);
        }


    }
}
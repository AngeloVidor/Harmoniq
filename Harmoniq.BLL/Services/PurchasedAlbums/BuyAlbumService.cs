using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Harmoniq.BLL.DTOs;
using Harmoniq.BLL.Interfaces.PurchasedAlbums;
using Harmoniq.DAL.Interfaces.ContentConsumerAccount;
using Harmoniq.DAL.Interfaces.PurchasedAlbums;
using Harmoniq.Domain.Entities;

namespace Harmoniq.BLL.Services.PurchasedAlbums
{
    public class BuyAlbumService : IBuyAlbumService
    {
        private readonly IBuyAlbumRepository _buyAlbumRepository;
        private readonly IContentConsumerAccountRepository _contentConsumerAccountRepository;
        private readonly IMapper _mapper;

        public BuyAlbumService(IBuyAlbumRepository buyAlbumRepository, IContentConsumerAccountRepository contentConsumerAccountRepository, IMapper mapper)
        {
            _buyAlbumRepository = buyAlbumRepository;
            _contentConsumerAccountRepository = contentConsumerAccountRepository;
            _mapper = mapper;
        }

        public async Task<PurchasedAlbumDto> BuyAlbumAsync(PurchasedAlbumDto albumDto)
        {
            if (albumDto == null)
            {
                throw new ArgumentNullException("albumDto cannot be null here");
            }
            var purchasedAlbumEntity = _mapper.Map<PurchasedAlbumEntity>(albumDto);
            var response = await _buyAlbumRepository.BuyAlbumAsync(purchasedAlbumEntity);
            return _mapper.Map<PurchasedAlbumDto>(response);
        }

        public async Task<AlbumDto> GetAlbumAsync(int albumId)
        {
            var album = await _buyAlbumRepository.GetAlbumAsync(albumId);
            return _mapper.Map<AlbumDto>(album);
        }
    }

}
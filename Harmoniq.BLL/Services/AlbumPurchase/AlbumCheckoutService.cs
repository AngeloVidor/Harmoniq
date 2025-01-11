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
    public class AlbumCheckoutService : IAlbumCheckoutService
    {
        private readonly IAlbumCheckoutRepository _checkoutAlbumRepository;
        private readonly IMapper _mapper;

        public AlbumCheckoutService(IAlbumCheckoutRepository checkoutAlbumRepository, IMapper mapper)
        {
            _checkoutAlbumRepository = checkoutAlbumRepository;
            _mapper = mapper;
        }

        public async Task<PurchasedAlbumDto> BuyAlbumAsync(PurchasedAlbumDto albumDto)
        {
            if (albumDto == null)
            {
                throw new ArgumentNullException("albumDto cannot be null here");
            }
            var purchasedAlbumEntity = _mapper.Map<PurchasedAlbumEntity>(albumDto);
            var response = await _checkoutAlbumRepository.BuyAlbumAsync(purchasedAlbumEntity);
            return _mapper.Map<PurchasedAlbumDto>(response);
        }
    }

}
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
            Console.WriteLine("Iniciando o processo de compra do álbum...");
            Console.WriteLine($"AlbumID: {albumDto.AlbumId}");
            Console.WriteLine($"ContentConsumerID: {albumDto.ContentConsumerId}");

            if (albumDto == null)
            {
                throw new ArgumentNullException("albumDto cannot be null here");
            }
            var purchasedAlbumEntity = _mapper.Map<PurchasedAlbumEntity>(albumDto);
            var response = await _buyAlbumRepository.BuyAlbumAsync(purchasedAlbumEntity);
            return _mapper.Map<PurchasedAlbumDto>(response);
        }
    }

}
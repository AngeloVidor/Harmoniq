using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Harmoniq.BLL.DTOs;
using Harmoniq.Domain.Entities;

namespace Harmoniq.BLL.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserEntity, UserRegisterDto>();
            CreateMap<UserRegisterDto, UserEntity>();

            CreateMap<UserLoginDto, UserRegisterDto>();

            CreateMap<ContentCreatorEntity, ContentCreatorDto>();
            CreateMap<ContentCreatorDto, ContentCreatorEntity>();

            CreateMap<AlbumEntity, AlbumDto>();
            CreateMap<AlbumDto, AlbumEntity>();

            CreateMap<AlbumSongsEntity, AlbumSongsDto>();
            CreateMap<AlbumSongsDto, AlbumSongsEntity>();

            CreateMap<ContentConsumerEntity, ContentConsumerDto>();
            CreateMap<ContentConsumerDto, ContentConsumerEntity>();

            CreateMap<UserDto, UserEntity>();

            CreateMap<PurchasedAlbumEntity, PurchasedAlbumDto>();
            CreateMap<PurchasedAlbumDto, PurchasedAlbumEntity>();

            CreateMap<CheckoutAlbumDto, AlbumDto>();

            CreateMap<AlbumDto, PurchasedAlbumDto>()
             .ForMember(dest => dest.AlbumId, opt => opt.MapFrom(src => src.Id.ToString()));

            CreateMap<PurchasedAlbumDto, CheckoutAlbumDto>();
            CreateMap<CheckoutAlbumDto, PurchasedAlbumDto>();

        }



    }
}

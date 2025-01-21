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
            CreateMap<UserEntity, UserDto>();


            CreateMap<PurchasedAlbumEntity, PurchasedAlbumDto>();
            CreateMap<PurchasedAlbumDto, PurchasedAlbumEntity>();

            CreateMap<CheckoutAlbumDto, AlbumDto>();

            CreateMap<AlbumDto, PurchasedAlbumDto>()
             .ForMember(dest => dest.AlbumId, opt => opt.MapFrom(src => src.Id.ToString()));

            CreateMap<PurchasedAlbumDto, CheckoutAlbumDto>();
            CreateMap<CheckoutAlbumDto, PurchasedAlbumDto>();

            CreateMap<PurchasedAlbumEntity, UserOwnedAlbumsDto>();
            CreateMap<UserOwnedAlbumsDto, PurchasedAlbumEntity>();

            CreateMap<FavoritesAlbumsDto, FavoritesAlbumsEntity>();
            CreateMap<FavoritesAlbumsEntity, FavoritesAlbumsDto>();

            CreateMap<WishlistEntity, WishlistDto>();
            CreateMap<WishlistDto, WishlistEntity>();

            CreateMap<FavoritesAlbumsEntity, GetFavoritesAlbumsDto>();

            CreateMap<WishlistEntity, GetWishlistDto>();

            CreateMap<CartEntity, CartDto>();
            CreateMap<CartDto, CartEntity>();

            CreateMap<CartAlbumEntity, CartAlbumDto>();
            CreateMap<CartAlbumDto, CartAlbumEntity>();

            CreateMap<CartCheckoutEntity, CartCheckoutDto>();
            CreateMap<CartCheckoutDto, CartCheckoutEntity>();

            CreateMap<AlbumSongsEntity, EditedAlbumSongsDto>();
            CreateMap<EditedAlbumSongsDto, AlbumSongsEntity>();

            CreateMap<AlbumEntity, EditedAlbumDto>();
            CreateMap<EditedAlbumDto, AlbumEntity>();

            CreateMap<CartAlbumEntity, EditCartAlbumDto>();
            CreateMap<EditCartAlbumDto, CartAlbumEntity>();

            CreateMap<ContentConsumerEntity, EditContentConsumerDto>();
            CreateMap<EditContentConsumerDto, ContentConsumerEntity>();

            CreateMap<ContentCreatorEntity, EditContentCreatorProfileDto>();
            CreateMap<EditContentCreatorProfileDto, ContentCreatorEntity>();

            CreateMap<StatisticsEntity, StatisticsDto>();
            CreateMap<StatisticsDto, StatisticsEntity>();

            CreateMap<StatisticsAlbumsEntity, StatisticsAlbumsDto>();
            CreateMap<StatisticsAlbumsDto, StatisticsAlbumsEntity>();

            CreateMap<AllPurchasedAlbumsDto, AllPurchasedAlbumsEntity>();
            CreateMap<AllPurchasedAlbumsEntity, AllPurchasedAlbumsDto>();

            CreateMap<FinalStatsDto, AllPurchasedAlbumsDto>();

            CreateMap<FollowersEntity, FollowersDto>();
            CreateMap<FollowersDto, FollowersEntity>();

            CreateMap<ContentCreatorEntity, ContentCreatorProfileDto>();
            CreateMap<ContentCreatorProfileDto, ContentCreatorEntity>();

            CreateMap<ContentConsumerEntity, ContentConsumerProfileDto>();
            CreateMap<ContentConsumerProfileDto, ContentConsumerEntity>();

            CreateMap<ReviewEntity, ReviewDto>();
            CreateMap<ReviewDto, ReviewEntity>();

            CreateMap<ReviewEntity, EditReviewDto>();
            CreateMap<EditReviewDto, ReviewEntity>();



        }


    }
}

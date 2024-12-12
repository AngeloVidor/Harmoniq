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

        }
    }
}
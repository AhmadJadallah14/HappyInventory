using AutoMapper;
using HappyInventory.Models.DTOs.User;
using HappyInventory.Models.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyInventory.Models.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role)).ReverseMap();


            CreateMap<User, NewUserDto>()
           .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
           .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
           .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))
           .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role)).ReverseMap();

            CreateMap<UpdateUserDto, User>()
           .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
           .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
           .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))
           .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role)).ReverseMap();
        }
    }
}

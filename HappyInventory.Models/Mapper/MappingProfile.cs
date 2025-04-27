using AutoMapper;
using HappyInventory.Models.DTOs.Logging;
using HappyInventory.Models.DTOs.User;
using HappyInventory.Models.DTOs.Warehouse;
using HappyInventory.Models.Models.Identity;
using HappyInventory.Models.Models.Warehouses;
using Serilog.Events;

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

            CreateMap<LogEvent, LogDto>()
                  .ForMember(dest => dest.Timestamp, opt => opt.MapFrom(src => src.Timestamp))
                  .ForMember(dest => dest.Level, opt => opt.MapFrom(src => Convert.ToString(src.Level)))
                  .ForMember(dest => dest.Exception, opt => opt.MapFrom(src => Convert.ToString(src.Exception)))
                  .AfterMap((src, dest) => dest.Message = src.RenderMessage());

            CreateMap<WarehouseDto, Warehouse>()
                .ForPath(dest => dest.Country.Name, opt => opt.MapFrom(src => src.CountryName))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                .ReverseMap();
        }
    }
}

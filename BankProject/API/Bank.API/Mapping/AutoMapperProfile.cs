using AutoMapper;
using Bank.API.DTOs;
using Bank.BusinessLayer.Models;

namespace BANK.BusinessLayer.Mapping;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<LoginRequest, Login>()
                .ForMember(dest => dest.Username,
                opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.Password,
                opt => opt.MapFrom(src => src.Password));

        CreateMap<RegisterRequest, Register>()
            .ForMember(dest => dest.Username,
            opt => opt.MapFrom(src => src.Username))
            .ForMember(dest => dest.Password,
            opt => opt.MapFrom(src => src.Password))
            .ForMember(dest => dest.Email,
            opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Role,
            opt => opt.MapFrom(src => src.Role));
    }
}

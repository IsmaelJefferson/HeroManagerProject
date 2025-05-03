using AutoMapper;
using HeroManager.Domain.Entities;
using HeroManager.Application.DTOs;

namespace HeroManager.Application.Mappings;

public class Mappingprofile : Profile
{
    public Mappingprofile()
    {
        CreateMap<Hero, HeroResponseDTO>()
        .ForMember(dest => dest.SuperPowers, opt => opt
        .MapFrom(src => src.SuperPowers.Select(sp => sp.SuperPower)));

        CreateMap<CreateHeroDTO, Hero>();

        CreateMap<UpdateHeroDTO, Hero>();

        CreateMap<SuperPower, SuperPowerDTO>();
    }


}
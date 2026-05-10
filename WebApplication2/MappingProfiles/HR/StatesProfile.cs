using AutoMapper;
using CRM.Domain.Entities;
using CRM.WebApp.DTOs.HumanResources;
using CRM.WebApp.ViewModels;

namespace CRM.WebApp.MappingProfiles.HR
{
    public class StatesProfile : Profile
    {
        public StatesProfile()
        {
            // Country
            CreateMap<Country, CountryDto>().ReverseMap();
            CreateMap<Country, CountryViewModel>().ReverseMap();

            // State
            CreateMap<State, StateDto>()
                .ForMember(dest => dest.CountryName, opt => opt.MapFrom(src => src.Country != null ? src.Country.Name : null))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country));
            CreateMap<StateDto, State>().ReverseMap();
            CreateMap<State, StateViewModel>()
                .ForMember(dest => dest.CountryName, opt => opt.MapFrom(src => src.Country != null ? src.Country.Name : null));
            CreateMap<StateViewModel, State>();

            // City
            CreateMap<City, CityDto>()
                .ForMember(dest => dest.StateName, opt => opt.MapFrom(src => src.State != null ? src.State.Name : null))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State));
            CreateMap<CityDto, City>().ReverseMap();
        }
    }
}

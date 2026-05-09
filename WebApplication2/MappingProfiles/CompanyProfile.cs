using AutoMapper;
using CRM.Domain.Entities;
using CRM.WebApp.DTOs.Company;
using CRM.WebApp.ViewModels.Company;

namespace CRM.WebApp.MappingProfiles
{
    public class CompanyProfile : Profile
    {
        public CompanyProfile()
        {
            // Entity to DTO
            CreateMap<Company, CompanyDto>()
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City != null ? src.City.Name : string.Empty))
                .ReverseMap()
                .ForMember(dest => dest.City, opt => opt.Ignore()) // Ignore navigation property when mapping from DTO
                .ForMember(dest => dest.CityId, opt => opt.Ignore()); // Ignore CityId - will be set separately if needed

            // DTO to Entity - Handle City string to CityId mapping
            CreateMap<CreateCompanyDto, Company>()
                .ForMember(dest => dest.City, opt => opt.Ignore()) // Ignore navigation property
                .ForMember(dest => dest.CityId, opt => opt.MapFrom(src => src.CityId)) // Use CityId from DTO
                .ForMember(dest => dest.Business, opt => opt.Ignore()) // Ignore navigation property
                .ForMember(dest => dest.User, opt => opt.Ignore()); // Ignore navigation property
            
            CreateMap<UpdateCompanyDto, Company>()
                .ForMember(dest => dest.City, opt => opt.Ignore()) // Ignore navigation property
                .ForMember(dest => dest.CityId, opt => opt.MapFrom(src => src.CityId)) // Use CityId from DTO
                .ForMember(dest => dest.Business, opt => opt.Ignore()) // Ignore navigation property
                .ForMember(dest => dest.User, opt => opt.Ignore()) // Ignore navigation property
                .ForMember(dest => dest.CreationDate, opt => opt.Ignore()); // Don't update creation date

            // DTO to ViewModel
            CreateMap<CompanyDto, CompanyViewModel>().ReverseMap();
            CreateMap<CreateCompanyDto, CreateCompanyViewModel>().ReverseMap();
            CreateMap<UpdateCompanyDto, EditCompanyViewModel>().ReverseMap();
        }
    }
}

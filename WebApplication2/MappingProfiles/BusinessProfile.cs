using AutoMapper;
using CRM.Domain.Entities;
using CRM.WebApp.DTOs.Business;
using CRM.WebApp.ViewModels.Business;

namespace CRM.WebApp.MappingProfiles
{
    public class BusinessProfile : Profile
    {
        public BusinessProfile()
        {
            // Entity to DTO
            CreateMap<Business, BusinessDto>().ReverseMap();

            // DTO to Entity
            CreateMap<CreateBusinessDto, Business>().ReverseMap();
            CreateMap<UpdateBusinessDto, Business>().ReverseMap();

            // DTO to ViewModel
            CreateMap<BusinessDto, BusinessViewModel>().ReverseMap();
            CreateMap<CreateBusinessDto, CreateBusinessViewModel>().ReverseMap();
            CreateMap<UpdateBusinessDto, EditBusinessViewModel>().ReverseMap();
        }
    }
}
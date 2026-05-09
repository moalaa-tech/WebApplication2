using AutoMapper;
using CRM.Domain.Entities.SalesManagement;
using CRM.WebApp.DTOs.Deal;

namespace CRM.WebApp.MappingProfiles
{
    // Mapping/DealProfile.cs
    public class DealProfile : Profile
    {
        public DealProfile()
        {
            CreateMap<Deal, DealDto>()
                .ForMember(dest => dest.OpportunityName, opt => opt.MapFrom(src => src.Opportunity.Name))
                .ForMember(dest => dest.ContactName, opt => opt.MapFrom(src => src.Contact.Name));

            CreateMap<DealFile, DealFileDto>();
            CreateMap<CreateDealDto, Deal>();
            CreateMap<UpdateDealDto, Deal>();
            CreateMap<DealDto, UpdateDealDto>();
        }
    }
}

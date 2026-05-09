using AutoMapper;
using CRM.Domain.Entities.MarketingAutomation;
using CRM.Domain.Entities.SalesManagement;
using CRM.WebApp.DTOs.Deal;
using CRM.WebApp.DTOs.Lead;
using CRM.WebApp.DTOs.MarketingAutomation;
using CRM.WebApp.DTOs.Opportunity;
using CRM.WebApp.ViewModels.SalesManagement;

namespace CRM.WebApp.MappingProfiles
{
    public class SalesProfile : Profile
    {
        public SalesProfile()
        {
            CreateMap<Deal, DealDto>()
                .ForMember(dest => dest.OpportunityName, opt => opt.MapFrom(src => src.Opportunity.Name))
                .ForMember(dest => dest.ContactName, opt => opt.MapFrom(src => src.Contact.Name));

            CreateMap<DealFile, DealFileDto>();
            CreateMap<CreateDealDto, Deal>();
            CreateMap<UpdateDealDto, Deal>();
            CreateMap<DealDto, UpdateDealDto>();


            CreateMap<Lead, LeadDto>()
                .ForMember(dest => dest.AssignedToUserName, opt => opt.MapFrom(src => src.AssignedToUser.UserName));

            CreateMap<LeadViewModel, Lead>()
                .ForMember(dest => dest.AssignedToUserId, opt => opt.MapFrom(src => src.AssignedToUserId))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ReverseMap();

            CreateMap<Opportunity, OpportunityDto>()
                .ForMember(dest => dest.OwnerUserName, opt => opt.MapFrom(src => src.OwnerUser.UserName))
                .ForMember(dest => dest.StageName, opt => opt.MapFrom(src => src.Stage.ToString()));

            CreateMap<LeadScore, LeadScoreDto>()
            .ForMember(dest => dest.CriteriaName, opt => opt.MapFrom(src => src.Criteria.Name))
            .ReverseMap();
            CreateMap<ScoreCriteria, ScoreCriteriaDto>().ReverseMap();

            // Other mappings...
        }
    }
}

using AutoMapper;
using CRM.Domain.Entities.MarketingAutomation;
using CRM.WebApp.DTOs.MarketingAutomation.Campaign;
using CRM.WebApp.ViewModels.MarketingAutomation;

namespace CRM.WebApp.MappingProfiles.MarketingAutomation
{
    public class CampaignProfile : Profile
    {
        public CampaignProfile()
        {
            CreateMap<Campaign, CampaignDto>()
                .ForMember(dest => dest.EmailTemplateName, opt => opt.MapFrom(src => src.EmailTemplate != null ? src.EmailTemplate.Name : null))
                .ForMember(dest => dest.ContactCount, opt => opt.MapFrom(src => src.Contacts.Count))
                .ForMember(dest => dest.InteractionCount, opt => opt.MapFrom(src => src.Interactions.Count));

            CreateMap<CampaignDto, Campaign>();

            CreateMap<Campaign, CampaignDetailsViewModel>()
                .ForMember(dest => dest.EmailTemplateName, opt => opt.MapFrom(src => src.EmailTemplate != null ? src.EmailTemplate.Name : "None"))
                .ForMember(dest => dest.ContactCount, opt => opt.MapFrom(src => src.Contacts.Count))
                .ForMember(dest => dest.InteractionCount, opt => opt.MapFrom(src => src.Interactions.Count));
        }
    }
}

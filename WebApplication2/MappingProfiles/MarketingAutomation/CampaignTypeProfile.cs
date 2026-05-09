using AutoMapper;
using CRM.Domain.Entities.MarketingAutomation;
using CRM.WebApp.DTOs.MarketingAutomation.Campaign;

namespace CRM.WebApp.MappingProfiles.MarketingAutomation
{
    public class CampaignTypeProfile : Profile
    {
        public CampaignTypeProfile()
        {
            CreateMap<CampaignTypes, CampaignTypeDto>();
            CreateMap<CampaignTypeDto, CampaignTypes>();
        }
    }
}

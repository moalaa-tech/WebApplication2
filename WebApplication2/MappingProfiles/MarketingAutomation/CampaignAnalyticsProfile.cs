using AutoMapper;
using CRM.Domain.Entities.MarketingAutomation;
using CRM.WebApp.DTOs.MarketingAutomation.CampaignAnalytics;

namespace CRM.WebApp.MappingProfiles.MarketingAutomation
{
    public class CampaignAnalyticsProfile : Profile
    {
        public CampaignAnalyticsProfile()
        {
            CreateMap<CampaignAnalytics, CampaignAnalyticsDto>();
            CreateMap<CampaignAnalyticsCreateDto, CampaignAnalytics>();
            CreateMap<CampaignAnalyticsUpdateDto, CampaignAnalytics>();
            CreateMap<CampaignAnalytics, CampaignAnalyticsUpdateDto>();
        }
    }
}

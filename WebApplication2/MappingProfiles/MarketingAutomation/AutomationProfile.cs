using AutoMapper;
using CRM.Domain.Entities.MarketingAutomation;
using CRM.WebApp.DTOs.Automation;

namespace CRM.WebApp.MappingProfiles.MarketingAutomation
{
    public class AutomationProfile : Profile
    {
        public AutomationProfile()
        {
            CreateMap<Automation, AutomationDto>().ReverseMap();
        }
    }

}

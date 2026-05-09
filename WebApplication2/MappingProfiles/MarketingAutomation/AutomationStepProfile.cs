using AutoMapper;
using CRM.Domain.Entities.MarketingAutomation;
using CRM.WebApp.DTOs.AutomationStep;
using CRM.WebApp.ViewModels.AutomationStep;

namespace CRM.WebApp.MappingProfiles.MarketingAutomation
{
    public class AutomationStepProfile : Profile
    {
        public AutomationStepProfile()
        {
            CreateMap<AutomationStep, AutomationStepDto>().ReverseMap();
            CreateMap<AutomationStepDto, AutomationStepViewModel>().ReverseMap();
        }
    }
}

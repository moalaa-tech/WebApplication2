using AutoMapper;
using CRM.Domain.Entities.MarketingAutomation;
using CRM.WebApp.DTOs.EmailTemplate;
using CRM.WebApp.ViewModels.EmailTemplate;

namespace CRM.WebApp.MappingProfiles.MarketingAutomation
{
    public class EmailTemplateProfile : Profile
    {
        public EmailTemplateProfile()
        {
            CreateMap<EmailTemplate, EmailTemplateDto>();

            CreateMap<EmailTemplateDto, EmailTemplateViewModel>();
        }
    }
}

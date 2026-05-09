using AutoMapper;
using CRM.Domain.Entities.MarketingAutomation;
using CRM.WebApp.DTOs.EmailTemplate;
using CRM.WebApp.ViewModels.EmailTemplate;

namespace CRM.WebApp.MappingProfiles
{
    public class EmailTemplateProfile : Profile
    {
        public EmailTemplateProfile()
        {
            CreateMap<EmailTemplate, EmailTemplateDto>().ReverseMap();
            CreateMap<EmailTemplate, EmailTemplateListDto>();

            // DTO to Entity for creation/update
            CreateMap<EmailTemplateCreateUpdateDto, EmailTemplate>();

            // DTO to ViewModel
            CreateMap<EmailTemplateDto, EmailTemplateViewModel>();
            CreateMap<EmailTemplateListDto, EmailTemplateListViewModel>();

            // ViewModel to DTO for creation/update
            CreateMap<EmailTemplateCreateEditViewModel, EmailTemplateCreateUpdateDto>();
        }
    }
}

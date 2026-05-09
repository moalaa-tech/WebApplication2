using AutoMapper;
using CRM.Domain.Entities.MarketingAutomation;
using CRM.Domain.Enums.MarketingAutomation;
using CRM.WebApp.DTOs.MarketingAutomation;
using CRM.WebApp.ViewModels.MarketingAutomation;

namespace CRM.WebApp.MappingProfiles.MarketingAutomation
{
    public class SocialMediaProfile : Profile
    {
        public SocialMediaProfile()
        {
            CreateMap<SocialMediaPost, SocialMediaPostViewModel>().ReverseMap();
            CreateMap<SocialMediaPostDto, SocialMediaPost>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => PostStatus.Draft))
                .ReverseMap();
        }
    }
}

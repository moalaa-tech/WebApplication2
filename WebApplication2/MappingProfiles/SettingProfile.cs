using AutoMapper;
using CRM.Domain.Entities;
using CRM.WebApp.ViewModels;

namespace CRM.WebApp.MappingProfiles
{
    public class SettingProfile : Profile
    {
        public SettingProfile()
        {
            CreateMap<Setting, SettingViewModel>();
            CreateMap<SettingViewModel, Setting>();
        }
    }
}
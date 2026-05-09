using AutoMapper;
using CRM.Domain.Entities.HR;
using CRM.WebApp.DTOs.HumanResources.JobTitle;
using CRM.WebApp.ViewModels.JobTitle;

namespace CRM.WebApp.MappingProfiles
{
    public class JobTitleProfile : Profile
    {
        public JobTitleProfile()
        {
            CreateMap<JobTitle, JobTitleViewModel>().ReverseMap();
            CreateMap<JobTitle, JobTitleDto>().ReverseMap();
            CreateMap<JobTitle, CreateJobTitleDto>().ReverseMap();
            CreateMap<JobTitle, UpdateJobTitleDto>().ReverseMap();
        }
    }
}

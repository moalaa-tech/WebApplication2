using AutoMapper;
using CRM.Domain.Entities.HR;
using CRM.WebApp.DTOs.HumanResources.JobApplication;

namespace CRM.WebApp.MappingProfiles.HR
{
    public class JobApplicationProfile : Profile
    {
        public JobApplicationProfile()
        {
            CreateMap<JobApplication, JobApplicationDto>()
                .ForMember(dest => dest.JobTitle, opt => opt.MapFrom(src => src.JobPosting.Title));

            CreateMap<JobApplication, CreateJobApplicationDto>()
               .ForMember(dest => dest.JobTitle, opt => opt.MapFrom(src => src.JobPosting.Title));

            CreateMap<JobApplication, UpdateJobApplicationDto>()
               .ForMember(dest => dest.JobTitle, opt => opt.MapFrom(src => src.JobPosting.Title));
        }
    }
}

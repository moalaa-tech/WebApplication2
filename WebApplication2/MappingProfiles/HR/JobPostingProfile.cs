using AutoMapper;
using CRM.Domain.Entities.HR;
using CRM.WebApp.DTOs.HumanResources.JobPosting;

namespace CRM.WebApp.MappingProfiles.HR
{
    public class JobPostingProfile : Profile
    {
        public JobPostingProfile()
        {
            CreateMap<JobPosting, JobPostingDto>()
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.Name))
                .ForMember(dest => dest.ApplicationCount, opt => opt.MapFrom(src => src.Applications.Count));


            CreateMap<JobPosting, CreateJobPostingDto>()
               .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.Name))
               .ForMember(dest => dest.ApplicationCount, opt => opt.MapFrom(src => src.Applications.Count));

            CreateMap<JobPosting, UpdateJobPostingDto>()
              .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.Name))
              .ForMember(dest => dest.ApplicationCount, opt => opt.MapFrom(src => src.Applications.Count));
        }
    }
}

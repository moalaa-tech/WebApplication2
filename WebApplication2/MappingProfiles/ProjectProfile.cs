using AutoMapper;
using CRM.Domain.Entities.ProjectManagment;
using CRM.WebApp.DTOs.Project;
using CRM.WebApp.ViewModels.Project;

namespace CRM.WebApp.MappingProfiles
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<Project, ProjectDto>()
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.Name))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

            CreateMap<CreateProjectDto, Project>().ReverseMap();
            CreateMap<UpdateProjectDto, Project>().ReverseMap();
            CreateMap<UpdateProjectDto, ProjectDto>().ReverseMap();

            CreateMap<Project, ProjectListDto>().ReverseMap();

            CreateMap<JobPhase, JobPhaseDto>().ReverseMap();
            CreateMap<CreateJobPhaseDto, JobPhase>().ReverseMap();

            // TimeEntry mappings
            CreateMap<TimeEntry, TimeEntryDto>()
                .ForMember(dest => dest.PhaseName, opt => opt.MapFrom(src => src.JobPhase.Name))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString())).ReverseMap();

            // PhaseExpense mappings
            CreateMap<PhaseExpense, PhaseExpenseDto>()
                .ForMember(dest => dest.PhaseName, opt => opt.MapFrom(src => src.JobPhase.Name))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.ToString()))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString())).ReverseMap();


            CreateMap<CreateProjectDto, ProjectCreateViewModel>().ReverseMap();


            CreateMap<ProjectDto, ProjectDetailsViewModel>().ReverseMap();

            CreateMap<ProjectCostSummaryDto, ProjectCostSummaryViewModel>().ReverseMap();



        }
    }
}

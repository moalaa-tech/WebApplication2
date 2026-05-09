using AutoMapper;
using CRM.Domain.Entities.ProjectManagment;
using CRM.WebApp.DTOs.FollowUpTask;

namespace CRM.WebApp.MappingProfiles
{
    public class FollowUpTaskProfile : Profile
    {
        public FollowUpTaskProfile()
        {
            CreateMap<FollowUpTask, FollowUpTaskDto>().ReverseMap();

        }
    }
}

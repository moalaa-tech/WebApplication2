using AutoMapper;
using CRM.Domain.Entities.HR;
using CRM.WebApp.DTOs.HumanResources.LeaveRequest;

namespace CRM.WebApp.MappingProfiles
{
    public class LeaveRequestProfile : Profile
    {
        public LeaveRequestProfile()
        {
            CreateMap<LeaveRequest, LeaveRequestDto>().ReverseMap();
            CreateMap<CreateLeaveRequestDto, LeaveRequest>().ReverseMap();
            CreateMap<UpdateLeaveRequestDto, LeaveRequest>().ReverseMap();
            CreateMap<LeaveRequestDto, UpdateLeaveRequestDto>().ReverseMap();
        }
    }
}

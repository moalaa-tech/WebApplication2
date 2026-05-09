using AutoMapper;
using CRM.Domain.Entities;
using CRM.WebApp.DTOs.LoggingDto;

namespace CRM.WebApp.MappingProfiles
{
    public class LogProfile : Profile
    {
        public LogProfile()
        {
            CreateMap<LogEntry, LogDto>()
                .ForMember(dest => dest.RequestBody, opt => opt.MapFrom(src =>
                    src.RequestBody != null && src.RequestBody.Length > 1000 ?
                    src.RequestBody.Substring(0, 1000) + "..." : src.RequestBody))
                .ForMember(dest => dest.ResponseBody, opt => opt.MapFrom(src =>
                    src.ResponseBody != null && src.ResponseBody.Length > 1000 ?
                    src.ResponseBody.Substring(0, 1000) + "..." : src.ResponseBody))
                .ForMember(dest => dest.Exception, opt => opt.MapFrom(src =>
                    src.Exception != null && src.Exception.Length > 2000 ?
                    src.Exception.Substring(0, 2000) + "..." : src.Exception)).ReverseMap();
        }
    }
}

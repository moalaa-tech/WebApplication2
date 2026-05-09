using AutoMapper;
using CRM.Domain.Entities;
using CRM.WebApp.DTOs.Notification;

namespace CRM.WebApp.MappingProfiles
{
    public class NotificationProfile : Profile
    {
        public NotificationProfile()
        {
            CreateMap<Notification, NotificationDto>().ReverseMap();
            CreateMap<CreateNotificationDto, Notification>();
            CreateMap<UpdateNotificationDto, Notification>()
                .ForMember(dest => dest.DateModified, opt => opt.Ignore())
                .ForMember(dest => dest.DateCreated, opt => opt.Ignore());
        }
    }
}

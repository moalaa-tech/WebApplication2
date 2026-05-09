using AutoMapper;
using CRM.WebApp.DTOs.CustomerService;
using CRM.WebApp.ViewModels.CustomerService;

namespace CRM.WebApp.MappingProfiles
{
    public class ServiceRequestProfile : Profile
    {
        public ServiceRequestProfile()
        {
            // Create mapping for creation
            CreateMap<CreateServiceRequestViewModel, CreateServiceRequestDto>();

            // Map between DTO and ViewModel for display/edit
            CreateMap<ServiceRequestDto, ServiceRequestViewModel>().ReverseMap();
        }
    }
}
using AutoMapper;
using CRM.Domain.Entities.HR;
using CRM.WebApp.DTOs.HumanResources.Employee;
using CRM.WebApp.ViewModels.Employee;

namespace CRM.WebApp.MappingProfiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeDto>().ReverseMap();


            CreateMap<CreateEmployeeDto, Employee>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}")).ReverseMap();


            CreateMap<UpdateEmployeeDto, Employee>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}")).ReverseMap();

            CreateMap<Employee, EmployeeViewModel>().ReverseMap();

        }
    }
}

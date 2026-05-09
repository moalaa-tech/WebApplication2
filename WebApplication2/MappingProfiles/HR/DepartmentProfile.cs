using AutoMapper;
using CRM.Domain.Entities.HR;
using CRM.WebApp.DTOs.HumanResources.Department;

namespace CRM.WebApp.MappingProfiles
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            // Map Department to DepartmentDto
            CreateMap<Department, DepartmentDto>()
                .ForMember(dest => dest.ManagerName, opt => opt.MapFrom(src => src.Manager != null ? $"{src.Manager.FirstName} {src.Manager.LastName}" : "N/A"));

            // Map CreateDepartmentDto to Department
            CreateMap<CreateDepartmentDto, Department>()
                .ForMember(dest => dest.DateCreated, opt => opt.MapFrom(src => DateTime.UtcNow));

            // Map UpdateDepartmentDto to Department
            CreateMap<UpdateDepartmentDto, Department>()
                .ForMember(dest => dest.DateModified, opt => opt.MapFrom(src => DateTime.UtcNow));


            // Department Mappings (Add these)
            CreateMap<Department, DepartmentDto>()
                .ForMember(dest => dest.EmployeeCount, opt => opt.MapFrom(src => src.Employees.Count))
                .ForMember(dest => dest.ManagerName, opt => opt.MapFrom(src => src.Manager != null ? src.Manager.Name : "N/A"));
            CreateMap<UpdateDepartmentDto, Department>();
        }

    }
}

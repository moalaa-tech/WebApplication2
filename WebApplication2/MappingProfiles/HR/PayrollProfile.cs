using AutoMapper;
using CRM.Domain.Entities.HR;
using CRM.WebApp.DTOs.HumanResources.Payroll;

namespace CRM.WebApp.MappingProfiles.HR
{
    public class PayrollProfile : Profile
    {
        public PayrollProfile()
        {
            CreateMap<Payroll, PayrollDto>()
                .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => $"{src.Employee.FirstName} {src.Employee.LastName}"));


            CreateMap<Payroll, CreatePayrollDto>()
                .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => $"{src.Employee.FirstName} {src.Employee.LastName}"))
                ;


            CreateMap<Payroll, UpdatePayrollDto>()
               .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => $"{src.Employee.FirstName} {src.Employee.LastName}"));


        }
    }
}

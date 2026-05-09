using AutoMapper;
using CRM.Domain.Entities.AccountsReceivable;
using CRM.WebApp.DTOs.Customer;
using CRM.WebApp.ViewModels.Customer;

namespace CRM.WebApp.MappingProfiles
{
    public class CustomerProfle : Profile
    {
        public CustomerProfle()
        {
            // --- New Customer Mappings ---
            CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<CreateCustomerDto, Customer>().ReverseMap();
            CreateMap<UpdateCustomerDto, Customer>().ReverseMap();

            // DTO to ViewModel
            CreateMap<CustomerDto, CustomerViewModel>().ReverseMap();
        }
    }
}

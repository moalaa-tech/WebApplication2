using AutoMapper;
using CRM.Domain.Entities.AccountsPayable;
using CRM.WebApp.DTOs.AccountsPayable;
using CRM.WebApp.ViewModels.AccountsPayable;

namespace CRM.WebApp.MappingProfiles.Accounting
{
    public class AccountsPayableProfile : Profile
    {
        public AccountsPayableProfile()
        {
            CreateMap<Invoice, InvoiceDto>().ReverseMap();
            CreateMap<Invoice, InvoiceViewModel>()
                .ForMember(dest => dest.VendorName, opt => opt.MapFrom(src => src.Vendor.Name));
            CreateMap<InvoiceViewModel, Invoice>();
        }
    }
}
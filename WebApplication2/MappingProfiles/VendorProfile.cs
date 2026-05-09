using AutoMapper;
using CRM.Domain.Entities.AccountsPayable;
using CRM.WebApp.DTOs.Vendor;

namespace CRM.WebApp.MappingProfiles
{
    public class VendorProfile : Profile
    {
        public VendorProfile()
        {
            CreateMap<Vendor, VendorDto>().ReverseMap();
            CreateMap<CreateVendorDto, Vendor>().ReverseMap();
            CreateMap<UpdateVendorDto, Vendor>().ReverseMap();
        }
    }

}

using AutoMapper;
using CRM.Domain.Entities.InventoryManagement;
using CRM.WebApp.DTOs.InventoryManagement;

namespace CRM.WebApp.MappingProfiles.InventoryManamgment
{
    public class InventoryProfile : Profile
    {
        public InventoryProfile()
        {
            CreateMap<Batch, BatchDto>().ReverseMap();
            CreateMap<ProductBarcode, BarcodeDto>().ReverseMap();
        }
    }
}
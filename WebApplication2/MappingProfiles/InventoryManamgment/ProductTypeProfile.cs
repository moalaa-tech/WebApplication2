using AutoMapper;
using CRM.Domain.Entities.InventoryManagement;
using CRM.WebApp.DTOs.InventoryManagement.ProductType;
using CRM.WebApp.ViewModels.InventoryManagement.ProductType;

namespace CRM.WebApp.MappingProfiles.InventoryManamgment
{
    public class ProductTypeProfile : Profile
    {
        public ProductTypeProfile()
        {
            CreateMap<ProductType, ProductTypeDto>().ReverseMap();


            CreateMap<CreateProductTypeDto, ProductType>().ReverseMap();
            CreateMap<CreateProductTypeDto, ProductTypeDto>().ReverseMap();
            CreateMap<UpdateProductTypeDto, ProductType>().ReverseMap();
            CreateMap<UpdateProductTypeDto, ProductTypeDto>().ReverseMap();

            // DTO to ViewModel
            CreateMap<ProductTypeDto, ProductTypeViewModel>().ReverseMap();
            CreateMap<CreateProductTypeDto, CreateProductTypeViewModel>().ReverseMap();
            CreateMap<UpdateProductTypeDto, EditProductTypeViewModel>().ReverseMap();
        }
    }
}

using AutoMapper;
using CRM.Domain.Entities.InventoryManagement;
using CRM.WebApp.DTOs.InventoryManagement.Product;
using CRM.WebApp.Paging;
using CRM.WebApp.ViewModels.InventoryManagement.Product;

namespace CRM.WebApp.MappingProfiles.InventoryManamgment
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.ProductTypeName, opt => opt.MapFrom(src => src.ProductType.Name));
            CreateMap<CreateProductDto, Product>();
            CreateMap<UpdateProductDto, Product>();

            CreateMap<UpdateProductDto, ProductDto>().ReverseMap();


            // DTO to ViewModel mappings
            CreateMap<ProductDto, ProductViewModel>().ReverseMap();
            //CreateMap<List<ProductDto>, List<ProductViewModel>>().ReverseMap();

            CreateMap<CreateProductDto, CreateProductViewModel>().ReverseMap();
            CreateMap<UpdateProductDto, EditProductViewModel>().ReverseMap();
        }
    }
}
using CRM.WebApp.DTOs.InventoryManagement.Product;
using CRM.WebApp.DTOs.InventoryManagement.ProductType;

namespace CRM.WebApp.Services.InventoryManagment
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();
        Task<ProductDto> GetProductByIdAsync(int id);
        Task<ProductDto> CreateProductAsync(CreateProductDto createProductDto);
        Task<bool> UpdateProductAsync(UpdateProductDto updateProductDto);
        Task<bool> DeleteProductAsync(int id);
        Task<bool> ProductExistsAsync(int id);
        Task<IEnumerable<ProductTypeDto>> GetAllProductTypesAsync(); // For dropdown        
    }
}

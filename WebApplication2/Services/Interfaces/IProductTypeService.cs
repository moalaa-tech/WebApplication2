using CRM.WebApp.DTOs.InventoryManagement.ProductType;

namespace CRM.WebApp.Services.Interfaces
{
    public interface IProductTypeService
    {
        Task<IEnumerable<ProductTypeDto>> GetAllAsync();
        Task<ProductTypeDto> GetByIdAsync(int id);
        Task<bool> CreateAsync(CreateProductTypeDto dto);
        Task<bool> UpdateAsync(UpdateProductTypeDto dto);
        Task<bool> DeleteAsync(int id);
    }
}

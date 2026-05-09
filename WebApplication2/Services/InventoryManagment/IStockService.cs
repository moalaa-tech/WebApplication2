using CRM.WebApp.DTOs.InventoryManagement;
using CRM.WebApp.DTOs.InventoryManagement.Product;

namespace CRM.WebApp.Services.InventoryManagment
{
    public interface IStockService
    {
        Task<List<ProductDto>> GetProductsAsync();
        Task<ProductDto?> GetProductAsync(int id);

        Task<ProductDto> CreateProductAsync(CreateProductDto dto);
        Task<bool> UpdateProductAsync(int id, UpdateProductDto dto);
        Task<bool> DeleteProductAsync(int id);

        Task<bool> StockInAsync(StockInDto dto);
        Task<bool> StockOutAsync(StockOutDto dto);
        Task<bool> AdjustStockAsync(StockAdjustmentDto dto);
    }
}

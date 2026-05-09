using CRM.WebApp.DTOs.InventoryManagement;
using CRM.WebApp.DTOs.InventoryManagement.Product;
using CRM.WebApp.Paging;

namespace CRM.WebApp.Services.Inventory
{
    public interface IInventoryService
    {
        Task<ProductDto> CreateItemAsync(ProductDto dto);
        Task<PaginatedList<ProductDto>> GetItemsAsync(int page = 1, int pageSize = 25);
        Task ReceiveGoodsAsync(StockTransactionDto dto);
        Task IssueGoodsAsync(StockTransactionDto dto);
        Task<int> GetCurrentStockAsync(int itemId, int? warehouseId = null);
        Task<IEnumerable<ProductDto>> GetReorderSuggestionsAsync();
        Task<IEnumerable<BatchDto>> GetBatchesForItemAsync(int itemId, int? warehouseId = null);
        Task<ProductDto?> LookupProductByBarcodeAsync(string code);
        Task<IEnumerable<WarehouseDto>> GetWarehousesAsync();
    }
}

using CRM.WebApp.DTOs.InventoryManagement;

namespace CRM.WebApp.Services.Inventory
{
    public interface IWarehouseService
    {
        Task<IEnumerable<WarehouseDto>> GetAllAsync();
        Task<WarehouseDto?> GetByIdAsync(int id);
        Task CreateAsync(WarehouseDto dto);
        Task UpdateAsync(WarehouseDto dto);
        Task DeleteAsync(int id);
    }
}

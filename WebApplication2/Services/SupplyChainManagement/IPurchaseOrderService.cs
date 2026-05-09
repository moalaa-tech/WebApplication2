using CRM.WebApp.DTOs.SupplyChainManagement.Procurement;
using CRM.WebApp.DTOs.SupplyChainManagement.Purchase;

namespace CRM.WebApp.Services.SupplyChainManagement
{
    public interface IPurchaseOrderService
    {
        Task<IEnumerable<PurchaseOrderDto>> GetAllPurchaseOrdersAsync();
        Task<PurchaseOrderDto> GetPurchaseOrderByIdAsync(int id);
        Task<PurchaseOrderDto> CreatePurchaseOrderAsync(CreatePurchaseOrderDto createPurchaseOrderDto);
        Task UpdatePurchaseOrderAsync(UpdatePurchaseOrderDto updatePurchaseOrderDto);
        Task DeletePurchaseOrderAsync(int id);
        Task<bool> PurchaseOrderExistsAsync(int id);
        Task<bool> PONumberExistsAsync(string poNumber, int? excludeId = null);
        Task ChangeStatusAsync(int id, string status);
    }
}

using CRM.WebApp.DTOs.SupplyChainManagement.Procurement;

namespace CRM.WebApp.Services.SupplyChainManagement
{
    public interface ISupplierService
    {
        Task<IEnumerable<SupplierDto>> GetAllSuppliersAsync();
        Task<SupplierDto> GetSupplierByIdAsync(int id);
        Task<SupplierDto> CreateSupplierAsync(CreateSupplierDto createSupplierDto);
        Task UpdateSupplierAsync(UpdateSupplierDto updateSupplierDto);
        Task DeleteSupplierAsync(int id);
        Task<bool> SupplierExistsAsync(int id);
    }
}

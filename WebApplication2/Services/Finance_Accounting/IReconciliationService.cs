using CRM.WebApp.DTOs.Banking;

namespace CRM.WebApp.Services.Finance_Accounting
{
    public interface IReconciliationService
    {
        Task<IEnumerable<ReconciliationDto>> GetAllAsync();
        Task<ReconciliationDto> GetByIdAsync(int id);
        Task DeleteAsync(int id);
        Task CreateAsync(CreateReconciliationDto dto);
        Task<IEnumerable<ReconciliationItemDto>> GetItemsAsync(int reconciliationId);
        Task CreateItemAsync(CreateReconciliationItemDto dto);
    }
}

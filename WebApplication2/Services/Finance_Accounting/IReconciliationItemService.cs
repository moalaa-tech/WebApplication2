using CRM.WebApp.DTOs.Banking;

namespace CRM.WebApp.Services.Finance_Accounting
{
    public interface IReconciliationItemService
    {
        Task<IEnumerable<ReconciliationItemDto>> GetAllByReconciliationIdAsync(int reconciliationId);
        Task<ReconciliationItemDto> GetByIdAsync(int id);
        Task CreateAsync(CreateReconciliationItemDto dto);
        Task DeleteAsync(int id);
        Task UpdateAsync(UpdateReconciliationItemDto vm);

    }
}

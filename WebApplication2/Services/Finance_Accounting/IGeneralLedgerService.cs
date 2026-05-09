using CRM.WebApp.DTOs.Accounting;

namespace CRM.WebApp.Services.Finance_Accounting
{
    public interface IGeneralLedgerService
    {
        Task<IEnumerable<GeneralLedgerDTO>> GetAllAsync();
        Task<GeneralLedgerDTO> GetByIdAsync(int id);
        Task CreateAsync(CreateGeneralLedgerDto generalLedgerDto);
        Task UpdateAsync(GeneralLedgerDTO generalLedgerDto);
        Task DeleteAsync(int id);
        Task PostTransactionAsync(int id);
    }
}

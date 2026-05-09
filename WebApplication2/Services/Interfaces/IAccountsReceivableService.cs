using CRM.WebApp.DTOs.Accounting;

namespace CRM.WebApp.Services.Interfaces
{
    public interface IAccountsReceivableService
    {
        Task<IEnumerable<AccountsReceivableDto>> GetAllAsync();
        Task<AccountsReceivableDto> GetByIdAsync(int id);
        Task CreateAsync(CreateAccountsReceivableDto dto);
        Task UpdateAsync(AccountsReceivableDto dto);
        Task DeleteAsync(int id);
    }
}
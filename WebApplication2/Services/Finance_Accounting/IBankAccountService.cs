using CRM.WebApp.DTOs.Banking;

namespace CRM.WebApp.Services.Finance_Accounting
{
    public interface IBankAccountService
    {
        Task<IEnumerable<BankAccountDto>> GetAllAsync();
        Task<BankAccountDto> GetByIdAsync(int id);
        Task CreateAsync(CreateBankAccountDto dto);
        Task UpdateAsync(UpdateBankAccountDto dto);
        Task DeleteAsync(int id);
    }
}

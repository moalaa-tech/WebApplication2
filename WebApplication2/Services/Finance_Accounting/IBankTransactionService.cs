using CRM.WebApp.DTOs.Banking;

namespace CRM.WebApp.Services.Finance_Accounting
{
    public interface IBankTransactionService
    {
        Task<IEnumerable<BankTransactionDto>> GetAllAsync();
        Task<BankTransactionDto> GetByIdAsync(int id);
        Task CreateAsync(CreateBankTransactionDto dto);
        Task DeleteAsync(int id);
    }
}

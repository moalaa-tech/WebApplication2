using CRM.WebApp.DTOs.Accounting;
using CRM.WebApp.Paging;

namespace CRM.WebApp.Services.Finance_Accounting
{
    public interface IExpenseService
    {
        Task<ExpenseDto> CreateAsync(CreateExpenseDto dto, int createdBy);
        Task<ExpenseDto?> GetByIdAsync(int id);
        Task<PagedResult<ExpenseDto>> GetPagedAsync(ExpenseQuery query);
        Task<bool> UpdateAsync(int id, CreateExpenseDto dto);
        Task<bool> DeleteAsync(int id);
        Task<MonthlyReport[]> GetMonthlyReportAsync(int year);
    }
}

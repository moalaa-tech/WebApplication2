using CRM.WebApp.DTOs.Accounting;

namespace CRM.WebApp.Services.Finance_Accounting
{
    public interface IExpenseCategoryService
    {
        Task<IEnumerable<ExpenseCategoryDto>> GetAllAsync();
        Task<ExpenseCategoryDto?> GetByIdAsync(int id);
        Task<ExpenseCategoryDto> CreateAsync(ExpenseCategoryCreateDto dto);
        Task<ExpenseCategoryDto?> UpdateAsync(int id, ExpenseCategoryUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}

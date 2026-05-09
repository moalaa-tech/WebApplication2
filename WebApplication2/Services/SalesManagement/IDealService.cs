using CRM.WebApp.DTOs.Deal;
using System.Collections;

namespace CRM.WebApp.Services.SalesManagement
{
    public interface IDealService
    {
        Task<IEnumerable<DealDto>> GetAllDealsAsync();
        Task<DealDto> GetDealByIdAsync(int id);
        Task<DealDto> CreateDealAsync(CreateDealDto createDto);
        Task<bool> UpdateDealAsync(int id, UpdateDealDto updateDto);
        Task<bool> DeleteDealAsync(int id);
        Task<string> SaveFileAsync(IFormFile file, string webRootPath);
        Task<IEnumerable> GetDealsAvailableForQuoteAsync(int id);
    }
}

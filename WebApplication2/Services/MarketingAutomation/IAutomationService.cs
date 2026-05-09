using CRM.WebApp.DTOs.Automation;

namespace CRM.WebApp.Services.MarketingAutomation
{
    public interface IAutomationService
    {
        Task<List<AutomationDto>> GetAllAsync();
        Task<AutomationDto?> GetByIdAsync(int id);
        Task AddAsync(CreateAutomationDto dto);
        Task UpdateAsync(UpdateAutomationDto dto);
        Task DeleteAsync(int id);
        Task SaveChangesAsync();
    }
}

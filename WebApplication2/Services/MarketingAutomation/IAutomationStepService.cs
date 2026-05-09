using CRM.WebApp.DTOs.AutomationStep;

namespace CRM.WebApp.Services.MarketingAutomation
{
    public interface IAutomationStepService
    {
        Task<IEnumerable<AutomationStepDto>> GetAllAsync();
        Task<AutomationStepDto> GetByIdAsync(int id);
        Task CreateAsync(CreateAutomationStepDto dto);
        Task UpdateAsync(UpdateAutomationStepDto dto);
        Task DeleteAsync(int id);

    }
}

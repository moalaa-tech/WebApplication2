using CRM.WebApp.DTOs.LeadScore;

namespace CRM.WebApp.Services.MarketingAutomation
{
    public interface ILeadScoreService
    {
        Task<IEnumerable<LeadScoreDto>> GetAllAsync();
        Task<LeadScoreDto> GetByIdAsync(int id);
        Task<LeadScoreDto> CreateAsync(LeadScoreCreateDto createDto);
        Task UpdateAsync(LeadScoreUpdateDto updateDto);
        Task DeleteAsync(int id);
        Task<IEnumerable<LeadScoreDto>> GetActiveScoresAsync();
        Task<IEnumerable<LeadScoreDto>> GetByTypeAsync(string scoreType);
        Task CalculateLeadScoreAsync(int leadId);
    }
}

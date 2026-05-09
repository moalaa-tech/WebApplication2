using CRM.WebApp.DTOs.Lead;
using CRM.WebApp.DTOs.MarketingAutomation;

namespace CRM.WebApp.Services.SalesManagement
{
    public interface ILeadService
    {
        Task<List<LeadDto>> GetLeadsAsync();
        Task<LeadDto?> GetLeadAsync(int id);
        Task<bool> CreateLeadAsync(LeadDto lead);
        Task<bool> UpdateLeadAsync(LeadDto lead);
        Task<bool> DeleteLeadAsync(int id);
        Task<LeadDto> GetLeadByIdAsync(int value);
        Task<int> CalculateTotalScoreAsync(int leadId);
        Task<IEnumerable<LeadScoreDto>> GetLeadScoresAsync(int leadId);
        Task AddScoreToLeadAsync(int leadId, int criteriaId, int? customPoints = null);
    }
}

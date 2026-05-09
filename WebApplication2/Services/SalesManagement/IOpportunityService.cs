using CRM.WebApp.DTOs.Opportunity;

namespace CRM.WebApp.Services.SalesManagement
{
    public interface IOpportunityService
    {
        Task<List<OpportunityDto>> GetOpportunitiesAsync();
        Task<OpportunityDto?> GetOpportunityAsync(int id);
        Task<bool> CreateOpportunityAsync(CreateOpportunityDto lead);
        Task<bool> UpdateOpportunityAsync(UpdateOpportunityDto lead);
        Task<bool> DeleteOpportunityAsync(int id);
        Task<OpportunityDto> GetOpportunityByIdAsync(int value);
    }
}

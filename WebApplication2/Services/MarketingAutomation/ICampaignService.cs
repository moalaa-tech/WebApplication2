using CRM.Domain.Enums.MarketingAutomation;
using CRM.WebApp.DTOs.MarketingAutomation.Campaign;

namespace CRM.WebApp.Services.MarketingAutomation
{
    public interface ICampaignService
    {
        Task<IEnumerable<CampaignDto>> GetAllCampaignsAsync();
        Task<CampaignDto> GetCampaignByIdAsync(int id);
        Task<CampaignDto> CreateCampaignAsync(CreateCampaignDto campaignDto);
        Task UpdateCampaignAsync(int id, UpdateCampaignDto campaignDto);
        Task DeleteCampaignAsync(int id);
        Task<bool> CampaignExistsAsync(int id);
        Task ChangeCampaignStatusAsync(int id, CampaignStatus status);
    }
}

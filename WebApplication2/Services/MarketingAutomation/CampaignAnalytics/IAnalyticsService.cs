using CRM.WebApp.DTOs.MarketingAutomation.CampaignAnalytics;

namespace CRM.WebApp.Services.MarketingAutomation.CampaignAnalytics
{
    public interface IAnalyticsService
    {
        Task<IEnumerable<CampaignAnalyticsDto>> GetAllAnalyticsAsync();
        Task<CampaignAnalyticsDto> GetAnalyticsByIdAsync(int id);
        Task<IEnumerable<CampaignAnalyticsDto>> GetAnalyticsByCampaignIdAsync(int campaignId);
        Task<IEnumerable<CampaignAnalyticsDto>> GetAnalyticsByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<CampaignAnalyticsDto>> GetAnalyticsByChannelAsync(string channel);
        Task<CampaignAnalyticsDto> AddAnalyticsAsync(CampaignAnalyticsCreateDto analyticsCreateDto);
        Task UpdateAnalyticsAsync(CampaignAnalyticsUpdateDto analyticsUpdateDto);
        Task DeleteAnalyticsAsync(int id);
        Task<decimal> CalculateTotalROIAsync();
        Task<decimal> CalculateChannelROIAsync(string channel);
        Task<Dictionary<string, decimal>> GetChannelPerformanceMetricsAsync();
    }
}

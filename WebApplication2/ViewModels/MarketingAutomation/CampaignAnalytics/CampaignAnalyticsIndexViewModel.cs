using CRM.WebApp.DTOs.MarketingAutomation.CampaignAnalytics;

namespace CRM.WebApp.ViewModels.MarketingAutomation.CampaignAnalytics
{
    public class CampaignAnalyticsIndexViewModel
    {
        public IEnumerable<CampaignAnalyticsDto> Analytics { get; set; }
        public Dictionary<string, decimal> ChannelPerformance { get; set; }
        public decimal TotalROI { get; set; }
    }
}

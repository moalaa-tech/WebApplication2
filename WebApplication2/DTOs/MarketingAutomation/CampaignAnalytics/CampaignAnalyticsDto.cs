namespace CRM.WebApp.DTOs.MarketingAutomation.CampaignAnalytics
{
    public class CampaignAnalyticsDto
    {
        public int Id { get; set; }
        public int CampaignId { get; set; }
        public string CampaignName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalBudget { get; set; }
        public decimal TotalSpent { get; set; }
        public int Impressions { get; set; }
        public int Clicks { get; set; }
        public int Conversions { get; set; }
        public decimal ConversionRate { get; set; }
        public decimal CostPerClick { get; set; }
        public decimal CostPerConversion { get; set; }
        public decimal RevenueGenerated { get; set; }
        public decimal ROIPercentage { get; set; }
        public DateTime RecordedDate { get; set; }
        public DateTime? LastUpdated { get; set; }
        public string Notes { get; set; }
        public string Channel { get; set; }
    }
}

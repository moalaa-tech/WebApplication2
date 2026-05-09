namespace CRM.WebApp.ViewModels.MarketingAutomation.CampaignAnalytics
{
    public class ROICalculatorViewModel
    {
        public decimal Investment { get; set; }
        public decimal Revenue { get; set; }
        public decimal CalculatedROI { get; set; }
        public Dictionary<string, decimal> ChannelROIs { get; set; }
    }
}

namespace CRM.WebApp.ViewModels.CustomerService.ServiceLevelAgreement
{
    public class SLAMetricsViewModel
    {
        public decimal ComplianceRate { get; set; }
        public int TotalBreaches { get; set; }
        public int TotalCases { get; set; }
        public decimal AverageResolutionTime { get; set; }
        public decimal AverageResponseTime { get; set; }
    }
}

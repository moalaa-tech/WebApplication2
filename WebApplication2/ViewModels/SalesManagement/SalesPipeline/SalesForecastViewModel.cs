namespace CRM.WebApp.ViewModels.SalesManagement.SalesPipeline
{
    public class SalesForecastViewModel
    {
        public Dictionary<string, decimal> MonthlyForecast { get; set; }
        public Dictionary<string, decimal> QuarterlyForecast { get; set; }
        public decimal TotalForecast { get; set; }
        public decimal ClosedWonAmount { get; set; }
        public decimal ProjectedAmount { get; set; }
    }
}

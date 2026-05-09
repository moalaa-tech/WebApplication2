namespace CRM.WebApp.DTOs.SalesPipeline
{
    public class SalesForecastDto
    {
        public Dictionary<string, decimal> MonthlyForecast { get; set; }
        public Dictionary<string, decimal> QuarterlyForecast { get; set; }
        public decimal TotalForecast { get; set; }
        public decimal ClosedWonAmount { get; set; }
        public decimal ProjectedAmount { get; set; }
    }
}

namespace CRM.WebApp.DTOs.SalesPipeline
{
    public class QuoteFilterDto
    {
        public string? SearchTerm { get; internal set; }
        public DateTime? ToDate { get; internal set; }
        public DateTime? FromDate { get; internal set; }
    }
}

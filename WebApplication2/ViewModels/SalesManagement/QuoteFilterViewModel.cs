
namespace CRM.WebApp.ViewModels.SalesManagement
{
    public class QuoteFilterViewModel
    {
        public DateTime? ToDate { get; internal set; }
        public string? SearchTerm { get; internal set; }
        public DateTime? FromDate { get; internal set; }
    }
}

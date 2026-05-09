namespace CRM.WebApp.DTOs.Accounting
{
    public class MonthlyReport { public int Month { get; set; } public decimal Total { get; set; } }


    public class ExpenseQuery
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        public int? CategoryId { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public string? Search { get; set; }
    }
}

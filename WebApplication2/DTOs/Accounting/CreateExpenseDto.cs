namespace CRM.WebApp.DTOs.Accounting
{
    public class CreateExpenseDto
    {
        public string Title { get; set; } = null!;
        public string TitleAR { get; set; } = null!;

        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public int? CategoryId { get; set; }
        public string? Notes { get; set; }
    }
}

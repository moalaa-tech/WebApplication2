namespace CRM.WebApp.DTOs.Accounting
{
    public record ExpenseDto(int Id, string Title, string TitleAR, decimal Amount, DateTime Date, int? CategoryId, string? CategoryName, string? Notes);
    
}

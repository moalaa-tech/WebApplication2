using CRM.WebApp.DTOs.Accounting;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CRM.WebApp.ViewModels.Accounting
{
    public class ExpenseListViewModel
    {
        public IEnumerable<ExpenseDto> Items { get; set; } = new List<ExpenseDto>();
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        // filters
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public int? CategoryId { get; set; }
        public IEnumerable<SelectListItem>? Categories { get; set; }
    }
}

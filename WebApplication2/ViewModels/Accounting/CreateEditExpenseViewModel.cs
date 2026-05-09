using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.ViewModels.Accounting
{
    public class CreateEditExpenseViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(200)]
        public string TitleAR { get; set; } = null!;

        [StringLength(500)]
        public string? Notes { get; set; }

        [Required]
        [Range(0.01, 10000000)]
        public decimal Amount { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; } = DateTime.Today;

        public int? CategoryId { get; set; }

        // For dropdown
        public IEnumerable<SelectListItem>? Categories { get; set; }
    }
}

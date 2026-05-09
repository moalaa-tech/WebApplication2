using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.ViewModels.Accounting
{
    public class CreateJournalEntryViewModel
    {
        [Required]
        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; } = DateTime.Today;

        [Required]
        [StringLength(500)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Debit")]
        [Range(0, double.MaxValue, ErrorMessage = "Debit amount must be greater than or equal to 0")]
        [DataType(DataType.Currency)]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Please enter a valid amount with up to 2 decimal places")]
        public decimal Debit { get; set; }

        [Required]
        [Display(Name = "Credit")]
        [Range(0, double.MaxValue, ErrorMessage = "Credit amount must be greater than or equal to 0")]
        [DataType(DataType.Currency)]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Please enter a valid amount with up to 2 decimal places")]
        public decimal Credit { get; set; }

        [StringLength(50)]
        [Display(Name = "Reference")]
        public string Reference { get; set; }
    }
}
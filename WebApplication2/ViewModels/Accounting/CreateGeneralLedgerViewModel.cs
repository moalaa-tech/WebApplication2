using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.ViewModels.Accounting
{
    public class CreateGeneralLedgerViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Transaction Date")]
        public DateTime TransactionDate { get; set; } = DateTime.Today;

        [Required]
        [StringLength(20)]
        [Display(Name = "Account Number")]
        public string AccountNumber { get; set; }

        [Required]
        [StringLength(255)]
        public string Description { get; set; }

        [Display(Name = "Debit Amount")]
        [Range(0, double.MaxValue)]
        public decimal DebitAmount { get; set; }

        [Display(Name = "Credit Amount")]
        [Range(0, double.MaxValue)]
        public decimal CreditAmount { get; set; }

        [StringLength(50)]
        public string Reference { get; set; }

        [Display(Name = "Is Posted")]
        public bool IsPosted { get; set; }
    }
}

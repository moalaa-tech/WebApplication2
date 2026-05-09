using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.DTOs.Accounting
{
    public class CreateAccountsReceivableDto
    {
        [Required]
        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }

        [Required]
        [Display(Name = "Invoice Number")]
        public string InvoiceNumber { get; set; }

        [Required]
        [Display(Name = "Invoice Date")]
        public DateTime InvoiceDate { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        [Display(Name = "Amount Due")]
        public decimal AmountDue { get; set; }

        [Display(Name = "Due Date")]
        public DateTime DueDate { get; set; }
    }
}
using CRM.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.ViewModels.AccountsPayable
{
    public class InvoiceViewModel
    {
        public int Id { get; set; }

        [Required]
        public int VendorId { get; set; }

        [Required]
        public string InvoiceNumber { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime InvoiceDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DueDate { get; set; }

        [Required]
        public decimal Amount { get; set; }

        public decimal PaidAmount { get; set; }

        public InvoiceStatus Status { get; set; }

        public string VendorName { get; set; }
    }
}
using CRM.Domain.Enums;

namespace CRM.WebApp.DTOs.AccountsPayable
{
    public class InvoiceDto
    {
        public int Id { get; set; }
        public int VendorId { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime DueDate { get; set; }
        public decimal Amount { get; set; }
        public decimal PaidAmount { get; set; }
        public InvoiceStatus Status { get; set; }
    }
}
using CRM.Domain.Base;
using CRM.Domain.Enums;

namespace CRM.Domain.Entities.AccountsPayable
{
    public class Invoice : BaseEntity
    {
        public int Id { get; set; }
        public int VendorId { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime DueDate { get; set; }
        public decimal Amount { get; set; }
        public decimal PaidAmount { get; set; }
        public InvoiceStatus Status { get; set; }

        public Vendor Vendor { get; set; }
        public ICollection<InvoiceLine> Lines { get; set; }
        public ICollection<Payment> Payments { get; set; }
    }
}

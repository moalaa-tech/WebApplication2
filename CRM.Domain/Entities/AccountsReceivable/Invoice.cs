using CRM.Domain.Base;
using CRM.Domain.Enums;

namespace CRM.Domain.Entities.AccountsReceivable
{
    public class Invoice : BaseEntity
    {
        public int CustomerId { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime DueDate { get; set; }
        public decimal Amount { get; set; }
        public decimal PaidAmount { get; set; }
        public InvoiceStatus Status { get; set; }

        public Customer Customer { get; set; }
        public ICollection<InvoiceLine> Lines { get; set; }
        public ICollection<Receipt> Receipts { get; set; }
    }
}

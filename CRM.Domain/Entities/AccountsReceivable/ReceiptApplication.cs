using CRM.Domain.Base;

namespace CRM.Domain.Entities.AccountsReceivable
{
    public class ReceiptApplication : BaseEntity
    {
        public int ReceiptId { get; set; }
        public int InvoiceId { get; set; }
        public decimal AmountApplied { get; set; }

        // Navigation properties
        public Receipt Receipt { get; set; }
        public Invoice Invoice { get; set; }
    }
}

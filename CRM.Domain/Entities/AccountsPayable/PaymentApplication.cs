using CRM.Domain.Base;

namespace CRM.Domain.Entities.AccountsPayable
{
    public class PaymentApplication : BaseEntity
    {
        public int PaymentId { get; set; }
        public int InvoiceId { get; set; }
        public decimal AmountApplied { get; set; }

        // Navigation properties
        public Payment Payment { get; set; }
        public Invoice Invoice { get; set; }
    }
}

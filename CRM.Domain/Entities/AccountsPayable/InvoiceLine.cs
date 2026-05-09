using CRM.Domain.Base;
using CRM.Domain.Entities.Accounting;


namespace CRM.Domain.Entities.AccountsPayable
{
    public class InvoiceLine : BaseEntity
    {
        public int InvoiceId { get; set; }
        public string Description { get; set; }
        public int GLAccountId { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TaxRate { get; set; }
        public decimal LineTotal => Quantity * UnitPrice * (1 + TaxRate / 100);

        // Navigation properties
        public Invoice Invoice { get; set; }
        public GLAccount GLAccount { get; set; }
    }
}

using CRM.Domain.Base;
using CRM.Domain.Entities.Banking;
using CRM.Domain.Enums;


namespace CRM.Domain.Entities.AccountsReceivable
{
    public class Receipt : BaseEntity
    {
        public int CustomerId { get; set; }
        public int? BankAccountId { get; set; }
        public string ReceiptNumber { get; set; }
        public DateTime ReceiptDate { get; set; }
        public decimal Amount { get; set; }
        public PaymentMethod Method { get; set; }
        public string Reference { get; set; }
        public string Memo { get; set; }

        // Navigation properties
        public Customer Customer { get; set; }
        public BankAccount BankAccount { get; set; }
        public ICollection<ReceiptApplication> Applications { get; set; }
    }
}

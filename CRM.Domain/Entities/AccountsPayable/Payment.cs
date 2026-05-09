using CRM.Domain.Base;
using CRM.Domain.Entities.Banking;
using CRM.Domain.Enums;


namespace CRM.Domain.Entities.AccountsPayable
{
    public class Payment : BaseEntity
    {
        public int VendorId { get; set; }
        public int? BankAccountId { get; set; }
        public string PaymentNumber { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }
        public PaymentMethod Method { get; set; }
        public string Reference { get; set; }
        public string Memo { get; set; }

        // Navigation properties
        public Vendor Vendor { get; set; }
        public BankAccount BankAccount { get; set; }
        public ICollection<PaymentApplication> Applications { get; set; }
    }
}

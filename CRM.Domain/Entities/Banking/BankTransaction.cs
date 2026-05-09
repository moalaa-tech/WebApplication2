using CRM.Domain.Base;
using CRM.Domain.Entities.AccountsPayable;
using CRM.Domain.Entities.AccountsReceivable;
using CRM.Domain.Enums;


namespace CRM.Domain.Entities.Banking
{
    public class BankTransaction : BaseEntity
    {
        public int BankAccountId { get; set; }
        public DateTime TransactionDate { get; set; }
        public DateTime? PostedDate { get; set; }
        public string ReferenceNumber { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public decimal RunningBalance { get; set; }
        public TransactionType Type { get; set; }
        public TransactionStatus Status { get; set; }
        public int? RelatedPaymentId { get; set; }
        public int? RelatedReceiptId { get; set; }

        // Navigation properties
        public BankAccount BankAccount { get; set; }
        public Payment RelatedPayment { get; set; }
        public Receipt RelatedReceipt { get; set; }
    }
}

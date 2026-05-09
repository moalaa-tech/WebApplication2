using CRM.Domain.Enums;

namespace CRM.WebApp.ViewModels.Banking
{
    public class CreateBankTransactionViewModel
    {
        public int BankAccountId { get; set; }
        public DateTime TransactionDate { get; set; }
        public string ReferenceNumber { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public TransactionType Type { get; set; }
        public TransactionStatus Status { get; set; }
    }
}

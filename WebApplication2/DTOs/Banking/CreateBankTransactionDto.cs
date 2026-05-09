using CRM.Domain.Enums;

namespace CRM.WebApp.DTOs.Banking
{
    public class CreateBankTransactionDto
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

using CRM.Domain.Base;

namespace CRM.Domain.Entities.Banking
{
    public class BankAccount : BaseEntity
    {
        public string AccountNumber { get; set; }
        public string BankName { get; set; }
        public string BankNameAr { get; set; }
        public string AccountName { get; set; }
        public string AccountNameAr { get; set; }
        public string Currency { get; set; }
        public decimal CurrentBalance { get; set; }

        public ICollection<BankTransaction> Transactions { get; set; }
        public ICollection<Reconciliation> Reconciliations { get; set; }
    }
}

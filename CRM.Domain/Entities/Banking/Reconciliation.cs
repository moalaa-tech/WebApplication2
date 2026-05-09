using CRM.Domain.Base;

namespace CRM.Domain.Entities.Banking
{
    public class Reconciliation : BaseEntity
    {
        public int BankAccountId { get; set; }
        public DateTime StatementDate { get; set; }
        public decimal StatementBalance { get; set; }
        public decimal AdjustedBookBalance { get; set; }
        public bool IsReconciled { get; set; }

        public BankAccount BankAccount { get; set; }
        public ICollection<ReconciliationItem> Items { get; set; }
    }
}

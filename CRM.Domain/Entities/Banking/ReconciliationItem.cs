using CRM.Domain.Base;
using CRM.Domain.Enums;

namespace CRM.Domain.Entities.Banking
{
    public class ReconciliationItem : BaseEntity
    {
        public int ReconciliationId { get; set; }
        public int BankTransactionId { get; set; }
        public ReconciliationStatus Status { get; set; }
        public decimal AdjustedAmount { get; set; }
        public string Notes { get; set; }

        // Navigation properties
        public Reconciliation Reconciliation { get; set; }
        public BankTransaction BankTransaction { get; set; }
    }
}

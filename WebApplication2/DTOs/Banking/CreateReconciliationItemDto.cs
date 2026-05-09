using CRM.Domain.Enums;

namespace CRM.WebApp.DTOs.Banking
{
    public class CreateReconciliationItemDto
    {
        public int ReconciliationId { get; set; }
        public int BankTransactionId { get; set; }
        public ReconciliationStatus Status { get; set; }
        public decimal AdjustedAmount { get; set; }
        public string Notes { get; set; }
    }
}

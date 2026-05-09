using CRM.Domain.Base;
using CRM.Domain.Enums;

namespace CRM.Domain.Entities.ProjectManagment
{
    public class PhaseExpense : BaseEntity
    {
        public int JobPhaseId { get; set; }
        public JobPhase JobPhase { get; set; }
        public string Description { get; set; }
        public DateTime ExpenseDate { get; set; }
        public decimal Amount { get; set; }
        public string Vendor { get; set; }
        public string ReceiptNumber { get; set; }
        public ExpenseCategory Category { get; set; }
        public bool IsBillable { get; set; }
        public ExpenseStatus Status { get; set; } // Enum: Entered, Submitted, Approved, Billed
    }
}

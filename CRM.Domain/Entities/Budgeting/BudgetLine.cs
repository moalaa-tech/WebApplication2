using CRM.Domain.Base;
using CRM.Domain.Entities.Accounting;

namespace CRM.Domain.Entities.Budgeting
{
    public class BudgetLine : BaseEntity
    {
        public int BudgetId { get; set; }
        public int GLAccountId { get; set; }
        public int PeriodMonth { get; set; } // 1-12
        public decimal BudgetAmount { get; set; }
        public decimal ActualAmount { get; set; }
        public decimal Variance => BudgetAmount - ActualAmount;

        // Navigation properties
        public Budget Budget { get; set; }
        public GLAccount GLAccount { get; set; }
    }
}

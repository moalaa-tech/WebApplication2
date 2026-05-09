using CRM.Domain.Base;

namespace CRM.Domain.Entities.Budgeting
{
    public class Budget : BaseEntity
    {
        public string Name { get; set; }
        public string NameAr { get; set; }
        public int FiscalYear { get; set; }
        public bool IsActive { get; set; }

        public ICollection<BudgetLine> Lines { get; set; }
    }
}

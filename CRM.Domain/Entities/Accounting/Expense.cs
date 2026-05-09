using CRM.Domain.Base;
using CRM.Domain.IdentityEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Domain.Entities.Accounting
{
    public class Expense : BaseEntity
    {
        public string Title { get; set; } = null!;
        public string TitleAR { get; set; } = null!;

        public string? Notes { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public int? CategoryId { get; set; }
        public ExpenseCategory? Category { get; set; }
        public int CreatedById { get; set; }
        public ApplicationUser? CreatedBy { get; set; }


        public int ModifiedById { get; set; }
        public ApplicationUser? ModifiedBy { get; set; }
    }
}

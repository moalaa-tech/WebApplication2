using CRM.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Domain.Entities.Accounting
{
    public class ExpenseCategory : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string NameAR { get; set; } = null!;

        public ICollection<Expense> Expenses { get; set; } = new List<Expense>();
    }
}

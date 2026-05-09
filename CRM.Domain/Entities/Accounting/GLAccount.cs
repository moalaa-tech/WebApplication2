using CRM.Domain.Base;
using CRM.Domain.Enums;

namespace CRM.Domain.Entities.Accounting
{
    public class GLAccount : BaseEntity
    {
        // General Ledger
        public string AccountCode { get; set; }
        public string Name { get; set; }
        public string NameAr { get; set; }
        public AccountType Type { get; set; }

        public ICollection<JournalLine> JournalLines { get; set; }
    }
}

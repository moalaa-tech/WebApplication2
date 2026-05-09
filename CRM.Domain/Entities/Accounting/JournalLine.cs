using CRM.Domain.Base;

namespace CRM.Domain.Entities.Accounting
{
    public class JournalLine : BaseEntity
    {
        public int JournalEntryId { get; set; }
        public int AccountId { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public string Description { get; set; }

        public JournalEntry JournalEntry { get; set; }
        public GLAccount Account { get; set; }
    }
}

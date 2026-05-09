using CRM.Domain.Base;

namespace CRM.Domain.Entities.Accounting
{
    public class JournalEntry : BaseEntity
    {
        public DateTime EntryDate { get; set; }
        public string Reference { get; set; }
        public string Description { get; set; }
        public bool IsPosted { get; set; }

        public ICollection<JournalLine> Lines { get; set; }
    }
}

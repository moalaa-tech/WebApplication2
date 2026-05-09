namespace CRM.WebApp.DTOs.Accounting
{
    public class JournalEntryDto
    {
        public int Id { get; set; }
        public DateTime EntryDate { get; set; }
        public string Reference { get; set; }
        public string Description { get; set; }
        public bool IsPosted { get; set; }
        public List<JournalLineDto> Lines { get; set; }
    }
}

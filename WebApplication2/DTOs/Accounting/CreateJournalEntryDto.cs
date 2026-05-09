namespace CRM.WebApp.DTOs.Accounting
{
    public class CreateJournalEntryDto
    {
        public IEnumerable<JournalLineDto> Lines { get; internal set; }
    }
}

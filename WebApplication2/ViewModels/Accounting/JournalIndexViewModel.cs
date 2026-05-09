namespace CRM.WebApp.ViewModels.Accounting
{
    public class JournalIndexViewModel
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public bool? PostedOnly { get; set; }
        public List<JournalEntryViewModel> Entries { get; set; }
    }
}

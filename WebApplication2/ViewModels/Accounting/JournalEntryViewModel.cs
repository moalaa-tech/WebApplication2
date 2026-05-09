namespace CRM.WebApp.ViewModels.Accounting
{
    public class JournalEntryViewModel
    {
        public int Id { get; set; }
        public string EntryDate { get; set; }
        public string Reference { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string TotalAmount { get; set; }
        public string Debit { get; set; }
        public string Credit { get; set; }
    }
}

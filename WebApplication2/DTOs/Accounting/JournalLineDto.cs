namespace CRM.WebApp.DTOs.Accounting
{
    public class JournalLineDto
    {
        public int AccountId { get; set; }
        public string AccountName { get; set; }
        public string AccountCode { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public string Description { get; set; }
    }
}

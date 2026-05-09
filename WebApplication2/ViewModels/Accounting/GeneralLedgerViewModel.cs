namespace CRM.WebApp.ViewModels.Accounting
{
    public class GeneralLedgerViewModel
    {
        public int Id { get; set; }
        public DateTime TransactionDate { get; set; }
        public string AccountNumber { get; set; }
        public string Description { get; set; }
        public decimal DebitAmount { get; set; }
        public decimal CreditAmount { get; set; }
        public string Reference { get; set; }
        public bool IsPosted { get; set; }
    }
}

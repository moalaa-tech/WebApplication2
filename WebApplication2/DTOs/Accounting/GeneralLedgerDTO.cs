namespace CRM.WebApp.DTOs.Accounting
{
    public class GeneralLedgerDTO
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

namespace CRM.WebApp.DTOs.Banking
{
    public class ReconciliationDto
    {
        public int Id { get; set; }
        public int BankAccountId { get; set; }
        public DateTime StatementDate { get; set; }
        public decimal StatementBalance { get; set; }
        public decimal AdjustedBookBalance { get; set; }
        public bool IsReconciled { get; set; }
    }
}

namespace CRM.WebApp.ViewModels.Banking
{
    public class CreateReconciliationViewModel
    {
        public int BankAccountId { get; set; }
        public DateTime StatementDate { get; set; }
        public decimal StatementBalance { get; set; }
        public decimal AdjustedBookBalance { get; set; }
        public bool IsReconciled { get; set; }
    }
}

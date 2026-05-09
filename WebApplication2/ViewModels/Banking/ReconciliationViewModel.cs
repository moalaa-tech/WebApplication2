namespace CRM.WebApp.ViewModels.Banking
{
    public class ReconciliationViewModel
    {
        public int Id { get; set; }
        public string BankName { get; set; }
        public DateTime StatementDate { get; set; }
        public decimal StatementBalance { get; set; }
        public decimal AdjustedBookBalance { get; set; }
        public bool IsReconciled { get; set; }
    }
}

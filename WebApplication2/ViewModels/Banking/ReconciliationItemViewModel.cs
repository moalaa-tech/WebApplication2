using CRM.Domain.Enums;

namespace CRM.WebApp.ViewModels.Banking
{
    public class ReconciliationItemViewModel
    {
        public int Id { get; set; }
        public string TransactionRef { get; set; }
        public decimal AdjustedAmount { get; set; }
        public ReconciliationStatus Status { get; set; }
        public string Notes { get; set; }
    }
}

using CRM.Domain.Enums;

namespace CRM.WebApp.ViewModels.Banking
{
    public class EditReconciliationItemViewModel
    {
        public int Id { get; set; }
        public decimal AdjustedAmount { get; set; }
        public ReconciliationStatus Status { get; set; }
        public string Notes { get; set; }
    }
}

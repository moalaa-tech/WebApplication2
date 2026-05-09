using CRM.Domain.Enums;

namespace CRM.WebApp.DTOs.Banking
{
    public class UpdateReconciliationItemDto
    {
        public int Id { get; set; }
        public decimal AdjustedAmount { get; set; }
        public ReconciliationStatus Status { get; set; }
        public string Notes { get; set; }
    }
}

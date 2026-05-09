namespace CRM.WebApp.DTOs.InventoryManagement
{
    public class ReorderRuleDto
    {
        public int ItemId { get; set; }
        public decimal MinQty { get; set; }
        public decimal MaxQty { get; set; }
        public decimal ReorderQty { get; set; }
    }
}

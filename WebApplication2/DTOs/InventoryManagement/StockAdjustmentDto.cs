using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.DTOs.InventoryManagement
{
    public class StockAdjustmentDto { [Required] public int ProductId { get; set; } [Range(0, int.MaxValue)] public int NewQuantity { get; set; } public string? Notes { get; set; } }

}

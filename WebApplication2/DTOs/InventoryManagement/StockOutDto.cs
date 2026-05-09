using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.DTOs.InventoryManagement
{
    public class StockOutDto { 
        [Required] 
        public int ProductId  { get; set; } 
        [Range(1, int.MaxValue)] 
        public int Quantity { get; set; } 
        public decimal? Cost { get; set; } 
        public string? Notes { get; set; } }

}

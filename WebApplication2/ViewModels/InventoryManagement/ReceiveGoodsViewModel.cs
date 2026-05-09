using Microsoft.AspNetCore.Mvc.Rendering;

namespace CRM.WebApp.ViewModels.InventoryManagement
{
    public class ReceiveGoodsViewModel
    {
        public int ItemId { get; set; }
        public int WarehouseId { get; set; }
        public decimal Quantity { get; set; }
        public string Reference { get; set; }

        // New: batch, expiry, barcode
        public string? BatchNumber { get; set; }
        public DateTime? ManufactureDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string? Barcode { get; set; }
        
        // Dropdown lists
        public IList<SelectListItem> Items { get; set; } = new List<SelectListItem>();
        public IList<SelectListItem> Warehouses { get; set; } = new List<SelectListItem>();
    }
}

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.ViewModels.SupplyChainManagement.Purchase
{
    public class PurchaseOrderItemViewModel
    {
        public int Id { get; set; }

        public int PurchaseOrderId { get; set; }

        [DisplayName("Item Name")]
        [Required(ErrorMessage = "Item Name is required")]
        [StringLength(100, ErrorMessage = "Item Name cannot exceed 100 characters")]
        public string ItemName { get; set; }

        [DisplayName("Item Code")]
        [StringLength(50, ErrorMessage = "Item Code cannot exceed 50 characters")]
        public string ItemCode { get; set; }

        [DisplayName("Quantity")]
        [Required(ErrorMessage = "Quantity is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
        public int Quantity { get; set; } = 1;

        [DisplayName("Unit Price")]
        [Required(ErrorMessage = "Unit Price is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Unit Price must be greater than 0")]
        [DataType(DataType.Currency)]
        public decimal UnitPrice { get; set; }

        [DisplayName("Total Price")]
        [DataType(DataType.Currency)]
        public decimal TotalPrice => Quantity * UnitPrice;

        [DisplayName("Description")]
        [StringLength(200, ErrorMessage = "Description cannot exceed 200 characters")]
        public string Description { get; set; }

    }
}

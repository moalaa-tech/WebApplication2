using CRM.WebApp.ViewModels.SupplyChainManagement.Supplier;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.ViewModels.SupplyChainManagement.Purchase
{
    public class CreatePurchaseOrderViewModel
    {
        [DisplayName("PO Number")]
        [Required(ErrorMessage = "PO Number is required")]
        [StringLength(20, ErrorMessage = "PO Number cannot exceed 20 characters")]
        public string PONumber { get; set; }

        [DisplayName("Order Date")]
        [Required(ErrorMessage = "Order Date is required")]
        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; } = DateTime.Today;

        [DisplayName("Expected Delivery Date")]
        [DataType(DataType.Date)]
        public DateTime? ExpectedDeliveryDate { get; set; }

        [DisplayName("Supplier")]
        [Required(ErrorMessage = "Supplier is required")]
        public int SupplierId { get; set; }

        [DisplayName("Description")]
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string Description { get; set; }

        public List<CreatePurchaseOrderItemViewModel> Items { get; set; } = new List<CreatePurchaseOrderItemViewModel>();

        public List<SupplierDropdownViewModel> AvailableSuppliers { get; set; } = new List<SupplierDropdownViewModel>();

    }
}

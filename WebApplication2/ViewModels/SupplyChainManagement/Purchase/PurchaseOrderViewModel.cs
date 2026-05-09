using CRM.WebApp.ViewModels.SupplyChainManagement.Supplier;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.ViewModels.SupplyChainManagement.Purchase
{
    public class PurchaseOrderViewModel
    {
        public int Id { get; set; }

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

        [DisplayName("Supplier Name")]
        public string SupplierName { get; set; }

        [DisplayName("Supplier Code")]
        public string SupplierCode { get; set; }

        [DisplayName("Description")]
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string Description { get; set; }

        [DisplayName("Total Amount")]
        [DataType(DataType.Currency)]
        public decimal TotalAmount { get; set; }

        [DisplayName("Status")]
        public string Status { get; set; } = "Draft";

        [DisplayName("Created Date")]
        public DateTime CreatedDate { get; set; }

        [DisplayName("Modified Date")]
        public DateTime? ModifiedDate { get; set; }

        public List<PurchaseOrderItemViewModel> Items { get; set; } = new List<PurchaseOrderItemViewModel>();

        // Helper properties for dropdowns
        public List<SupplierDropdownViewModel> AvailableSuppliers { get; set; } = new List<SupplierDropdownViewModel>();
        public List<string> AvailableStatuses { get; set; } = new List<string>
        {
            "Draft",
            "Submitted",
            "Approved",
            "Ordered",
            "Received",
            "Completed",
            "Cancelled"
        };

    }
}

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CRM.WebApp.ViewModels.InventoryManagement.Product
{
    public class CreateProductViewModel
    {
        [Required(ErrorMessage = "Product name is required.")]
        [StringLength(200, ErrorMessage = "Product name cannot exceed 200 characters.")]
        [DisplayName("Product Name")]
        public string Name { get; set; }

        [StringLength(200, ErrorMessage = "Arabic product name cannot exceed 200 characters.")]
        [DisplayName("Arabic Product Name")]
        public string NameAr { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Total Cost must be a non-negative value.")]
        [DisplayName("Total Cost")]
        [DataType(DataType.Currency)]
        public decimal TotalCost { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Shipping Cost must be a non-negative value.")]
        [DisplayName("Shipping Cost")]
        [DataType(DataType.Currency)]
        public decimal ShippingCost { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Freight Cost must be a non-negative value.")]
        [DisplayName("Freight Cost")]
        [DataType(DataType.Currency)]
        public decimal FreightCost { get; set; }

        [DisplayName("Product Type")]
        public int? ProductTypeId { get; set; }

        [DisplayName("Upload Image")]
        public IFormFile ImageFileUpload { get; set; }
        public string? ImageFile { get; set; }

        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters.")]
        [DisplayName("Description")]
        public string Description { get; set; }

        // For dropdown lists
        public List<SelectListItem> ProductTypes { get; set; } = new List<SelectListItem>();
    }
}
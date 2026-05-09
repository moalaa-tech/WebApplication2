using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CRM.WebApp.ViewModels.InventoryManagement.Product
{
    public class EditProductViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Product name is required.")]
        [StringLength(200, ErrorMessage = "Product name cannot exceed 200 characters.")]
        public string Name { get; set; }

        [StringLength(200, ErrorMessage = "Arabic product name cannot exceed 200 characters.")]
        public string NameAr { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Total Cost must be a non-negative value.")]
        [DataType(DataType.Currency)]
        public decimal TotalCost { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Shipping Cost must be a non-negative value.")]
        [DataType(DataType.Currency)]
        public decimal ShippingCost { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Freight Cost must be a non-negative value.")]
        [DataType(DataType.Currency)]
        public decimal FreightCost { get; set; }

        public int? ProductTypeId { get; set; }

        public string ExistingImageFile { get; set; }
        
        public IFormFile NewImageFile { get; set; }

        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters.")]
        public string Description { get; set; }

        // For dropdown lists
        public List<SelectListItem> ProductTypes { get; set; } = new List<SelectListItem>();
    }
}
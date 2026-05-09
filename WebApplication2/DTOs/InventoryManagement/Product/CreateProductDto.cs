using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.DTOs.InventoryManagement.Product
{
    public class CreateProductDto
    {
        [Required(ErrorMessage = "Product name is required.")]
        [StringLength(200, ErrorMessage = "Product name cannot exceed 200 characters.")]
        public string Name { get; set; }

        [StringLength(200, ErrorMessage = "Arabic product name cannot exceed 200 characters.")]
        public string NameAr { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Total Cost must be a non-negative value.")]
        public decimal TotalCost { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Shipping Cost must be a non-negative value.")]
        public decimal ShippingCost { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Freight Cost must be a non-negative value.")]
        public decimal FreightCost { get; set; }

        public int? ProductTypeId { get; set; }

        public IFormFile ImageFileUpload { get; set; } // For uploading a new image
        public string? ImageFile { get; set; }

        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters.")]
        public string Description { get; set; }
    }
}

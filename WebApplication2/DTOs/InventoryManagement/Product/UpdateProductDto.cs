using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.DTOs.InventoryManagement.Product
{
    public class UpdateProductDto
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Product name is required.")]
        [StringLength(200, ErrorMessage = "Product name cannot exceed 200 characters.")]
        public string Name { get; set; }

        [StringLength(200, ErrorMessage = "Arabic product name cannot exceed 200 characters.")]
        public string NameAr { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Total Cost must be a non-negative value.")]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]

        public decimal? TotalCost { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Shipping Cost must be a non-negative value.")]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]

        public decimal? ShippingCost { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Freight Cost must be a non-negative value.")]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public decimal? FreightCost { get; set; }

        public int? ProductTypeId { get; set; }

        public string? ExistingImageFile { get; set; } // To show the current image if any
        //public IFormFile? NewImageFile { get; set; } // For uploading a new image

        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters.")]
        public string? Description { get; set; }

    }
}

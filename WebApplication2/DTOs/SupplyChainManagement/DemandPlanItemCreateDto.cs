using CRM.WebApp.Attributes;
using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.DTOs.SupplyChainManagement
{
    public class DemandPlanItemCreateDto
    {
        [Required(ErrorMessage = "Demand plan ID is required")]
        public int DemandPlanId { get; set; }

        [Required(ErrorMessage = "Product ID is required")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Period is required")]
        [DataType(DataType.Date)]
        [FutureDateValidation(ErrorMessage = "Period cannot be in the past")]
        public DateTime Period { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "Quantity is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Quantity must be at least 0.01")]
        public decimal Quantity { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be at least 0.01 if provided")]
        public decimal? Price { get; set; }

        [StringLength(200, ErrorMessage = "Notes cannot exceed 200 characters")]
        public string Notes { get; set; }

        [Required(ErrorMessage = "Creator identifier is required")]
        public string CreatedBy { get; set; }

    }
}

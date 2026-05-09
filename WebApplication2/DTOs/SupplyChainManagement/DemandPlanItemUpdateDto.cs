using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.DTOs.SupplyChainManagement
{
    public class DemandPlanItemUpdateDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int DemandPlanId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Period is required")]
        [DataType(DataType.Date)]
        public DateTime Period { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Quantity must be a positive number")]
        public decimal Quantity { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive number")]
        public decimal? Price { get; set; }

        [StringLength(200, ErrorMessage = "Notes cannot exceed 200 characters")]
        public string Notes { get; set; }

        [Required]
        public string UpdatedBy { get; set; }

        [Required]
        public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;

        // Version for optimistic concurrency
        [Required]
        public int Version { get; set; }
    }
}

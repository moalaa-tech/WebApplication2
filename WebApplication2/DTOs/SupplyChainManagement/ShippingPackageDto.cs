using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.DTOs.SupplyChainManagement
{
    public class ShippingPackageDto
    {
        [Required(ErrorMessage = "Weight is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Weight must be at least 0.01")]
        public decimal Weight { get; set; }

        [Required(ErrorMessage = "Weight unit is required")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "Weight unit must be 2 characters")]
        public string WeightUnit { get; set; } = "kg";

        [Range(0.01, double.MaxValue, ErrorMessage = "Length must be at least 0.01")]
        public decimal? Length { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Width must be at least 0.01")]
        public decimal? Width { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Height must be at least 0.01")]
        public decimal? Height { get; set; }

        [StringLength(2, MinimumLength = 2, ErrorMessage = "Dimension unit must be 2 characters")]
        public string DimensionUnit { get; set; } = "cm";

        [StringLength(100, ErrorMessage = "Description cannot exceed 100 characters")]
        public string Description { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Value must be positive")]
        public decimal Value { get; set; }

        [StringLength(3, MinimumLength = 3, ErrorMessage = "Currency must be 3 characters")]
        public string Currency { get; set; } = "USD";

        [StringLength(50, ErrorMessage = "Package type cannot exceed 50 characters")]
        public string PackageType { get; set; }

        public bool IsHazardous { get; set; }
        public bool IsFragile { get; set; }
        public bool RequiresSpecialHandling { get; set; }

        // Calculated properties (read-only)
        public decimal Volume => (Length ?? 0) * (Width ?? 0) * (Height ?? 0);
        public decimal VolumetricWeight => Volume > 0 ? Volume / 5000 : 0; // Standard volumetric divisor
        public decimal ChargeableWeight => Weight > VolumetricWeight ? Weight : VolumetricWeight;
    }
}


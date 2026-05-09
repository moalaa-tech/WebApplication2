using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.DTOs.SupplyChainManagement
{
    public class PackageCreateDto
    {
        [Required, Range(0.01, double.MaxValue)]
        public decimal Weight { get; set; }

        [Required, MaxLength(2)]
        public string WeightUnit { get; set; } = "kg";

        [Range(0.01, double.MaxValue)]
        public decimal Length { get; set; }

        [Range(0.01, double.MaxValue)]
        public decimal Width { get; set; }

        [Range(0.01, double.MaxValue)]
        public decimal Height { get; set; }

        [MaxLength(2)]
        public string DimensionUnit { get; set; } = "cm";

        [MaxLength(100)]
        public string Description { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Value { get; set; }

        [MaxLength(3)]
        public string Currency { get; set; } = "USD";
    }
}

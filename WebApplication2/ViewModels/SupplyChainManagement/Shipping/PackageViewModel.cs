using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.ViewModels.SupplyChainManagement.Shipping
{
    public class PackageViewModel
    {
        [Required]
        [Range(0.1, 1000)]
        public decimal Weight { get; set; } // in lbs or kgs

        [Range(1, 200)]
        public decimal Length { get; set; } // in inches or cm

        [Range(1, 200)]
        public decimal Width { get; set; } // in inches or cm

        [Range(1, 200)]
        public decimal Height { get; set; } // in inches or cm
    }
}

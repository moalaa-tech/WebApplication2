using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.DTOs.SupplyChainManagement
{
    public class ShippingCreateDto
    {
        [Required]
        public AddressCreateDto Origin { get; set; }

        [Required]
        public AddressCreateDto Destination { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "At least one package is required")]
        public List<PackageCreateDto> Packages { get; set; } = new List<PackageCreateDto>();

        [Required]
        public string CarrierId { get; set; }

        [Required]
        public string ServiceId { get; set; }

        public DateTime? ShipDate { get; set; } = DateTime.UtcNow.Date;

        public bool RequiresSignature { get; set; }

        public bool IsInsured { get; set; }

        [Range(0, double.MaxValue)]
        public decimal InsuranceValue { get; set; }

        public string CustomerReference { get; set; }

        [Required]
        public string CreatedBy { get; set; }
    }
}

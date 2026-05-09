using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.DTOs.SupplyChainManagement
{
    public class ShippingUpdateDto
    {
        [Required]
        public Guid Id { get; set; }

        public DateTime? ShipDate { get; set; }

        public string Status { get; set; }

        public DateTime? ActualDeliveryDate { get; set; }

        [MaxLength(500)]
        public string TrackingNotes { get; set; }

        public bool? RequiresSignature { get; set; }

        public bool? IsInsured { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? InsuranceValue { get; set; }

        [Required]
        public string UpdatedBy { get; set; }
    }
}

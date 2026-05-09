using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.DTOs.SupplyChainManagement.ForecastData
{
    public class ForecastDataDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string DataType { get; set; }

        [Required]
        public DateTime DataDate { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Value { get; set; }

        [Range(0, double.MaxValue)]
        public decimal ValueDecimal { get; set; }

        [StringLength(500)]
        public string Notes { get; set; }

        public int DemandPlanId { get; set; }
        public int? SupplyChainEventId { get; set; }
        public int? ItemId { get; set; }
        public int? ShippingId { get; set; }
        public int? FreightId { get; set; }
        public int? HistoricalDataId { get; set; }

        // Navigation properties for display
        public string DemandPlanName { get; set; }
        public string SupplyChainEventName { get; set; }
        public string ItemName { get; set; }
        public string ShippingReference { get; set; }
        public string FreightReference { get; set; }
        public string HistoricalDataPeriod { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}

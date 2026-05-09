using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.DTOs.SupplyChainManagement.ForecastData
{
    public class CreateForecastDataDto
    {
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

        [Required]
        public int DemandPlanId { get; set; }
        public int? SupplyChainEventId { get; set; }
        public int? ItemId { get; set; }
        public int? ShippingId { get; set; }
        public int? FreightId { get; set; }
        public int? HistoricalDataId { get; set; }
    }
}

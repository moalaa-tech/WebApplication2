using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using _DataType = System.ComponentModel.DataAnnotations.DataType;

namespace CRM.WebApp.ViewModels.SupplyChainManagement.ForecastData
{
    public class ForecastDataViewModel
    {
        public int Id { get; set; }

        [DisplayName("Data Type")]
        [Required(ErrorMessage = "Data Type is required")]
        [StringLength(50, ErrorMessage = "Data Type cannot exceed 50 characters")]
        public string DataType { get; set; }

        [DisplayName("Data Date")]
        [Required(ErrorMessage = "Data Date is required")]
        [DataType(_DataType.Date)]
        public DateTime DataDate { get; set; } = DateTime.Today;

        [DisplayName("Value")]
        [Required(ErrorMessage = "Value is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Value must be positive")]
        public decimal Value { get; set; }

        [DisplayName("Value (Decimal)")]
        [Range(0, double.MaxValue, ErrorMessage = "Value must be positive")]
        public decimal ValueDecimal { get; set; }

        [DisplayName("Notes")]
        [StringLength(500, ErrorMessage = "Notes cannot exceed 500 characters")]
        public string Notes { get; set; }

        [DisplayName("Demand Plan")]
        [Required(ErrorMessage = "Demand Plan is required")]
        public int DemandPlanId { get; set; }

        [DisplayName("Supply Chain Event")]
        public int? SupplyChainEventId { get; set; }

        [DisplayName("Item")]
        public int? ItemId { get; set; }

        [DisplayName("Shipping")]
        public int? ShippingId { get; set; }

        [DisplayName("Freight")]
        public int? FreightId { get; set; }

        [DisplayName("Historical Data")]
        public int? HistoricalDataId { get; set; }

        // Display properties
        [DisplayName("Demand Plan")]
        public string DemandPlanName { get; set; }

        [DisplayName("Supply Chain Event")]
        public string SupplyChainEventName { get; set; }

        [DisplayName("Item")]
        public string ItemName { get; set; }

        [DisplayName("Shipping Reference")]
        public string ShippingReference { get; set; }

        [DisplayName("Freight Reference")]
        public string FreightReference { get; set; }

        [DisplayName("Historical Period")]
        public string HistoricalDataPeriod { get; set; }

        [DisplayName("Created Date")]
        public DateTime CreatedDate { get; set; }

        [DisplayName("Modified Date")]
        public DateTime? ModifiedDate { get; set; }

        // Dropdown lists
        public List<DropdownOption> DataTypes { get; set; } = new List<DropdownOption>
        {
            new DropdownOption { Value = 1, Text = "Sales" },
            new DropdownOption { Value = 2, Text = "Inventory" },
            new DropdownOption { Value = 3, Text = "Demand" },
            new DropdownOption { Value = 4, Text = "Production" },
            new DropdownOption { Value = 5, Text = "Capacity" }
        };

        public List<DropdownOption> DemandPlans { get; set; } = new List<DropdownOption>();
        public List<DropdownOption> SupplyChainEvents { get; set; } = new List<DropdownOption>();
        public List<DropdownOption> Items { get; set; } = new List<DropdownOption>();
        public List<DropdownOption> ShippingOptions { get; set; } = new List<DropdownOption>();
        public List<DropdownOption> FreightOptions { get; set; } = new List<DropdownOption>();
        public List<DropdownOption> HistoricalDataOptions { get; set; } = new List<DropdownOption>();
    }
}

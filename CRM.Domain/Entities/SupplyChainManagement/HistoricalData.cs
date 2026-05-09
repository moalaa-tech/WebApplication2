using CRM.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Domain.Entities.SupplyChainManagement
{
    public class HistoricalData : BaseEntity
    {
        public HistoricalData() { }
        public string DataType { get; set; } // e.g., "Sales", "Inventory", "Demand"
        public DateTime DataDate { get; set; } // Date of the historical data
            
        public decimal Value { get; set; } // Value of the historical data
        public string Notes { get; set; } // Additional notes or comments about the data
        public int DemandPlanId { get; set; } // Foreign key to DemandPlan
        public DemandPlan DemandPlan { get; set; } // Navigation property to DemandPlan
        public int? SupplyChainEventId { get; set; } // Optional foreign key to SupplyChainEvent
        public SupplyChainEvent SupplyChainEvent { get; set; } // Navigation property to SupplyChainEvent
        public int? ForecastDataId { get; set; } // Optional foreign key to ForecastData
        public ForecastData ForecastData { get; set; } // Navigation property to ForecastData
        public int? ItemId { get; set; } // Optional foreign key to DemandPlanItem
        public DemandPlanItem Item { get; set; } // Navigation property to DemandPlanItem
        public int? ShippingId { get; set; } // Optional foreign key to Shipping
        public Shipping Shipping { get; set; } // Navigation property to Shipping
        public int? FreightId { get; set; } // Optional foreign key to Freight
        public Freight Freight { get; set; } // Navigation property to Freight

    }
}

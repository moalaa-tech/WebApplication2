using CRM.Domain.Base;
using CRM.Domain.Enums.SupplyChainManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Domain.Entities.SupplyChainManagement
{
    public class DemandPlan : BaseEntity
    {
        public DemandPlan()
        {
            Items = new List<DemandPlanItem>();
            SupplyChainEvents = new List<SupplyChainEvent>();
            HistoricalData = new List<HistoricalData>();
            ForecastData = new List<ForecastData>();
        }


        public string PlanName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DemandPlanStatus Status { get; set; }
        public string Notes { get; set; }

        public ICollection<DemandPlanItem> Items { get; set; }
        public ICollection<SupplyChainEvent> SupplyChainEvents { get; set; } 
        public ICollection<HistoricalData> HistoricalData { get; set; }
        public ICollection<ForecastData> ForecastData { get; set; } 

        
    }
}

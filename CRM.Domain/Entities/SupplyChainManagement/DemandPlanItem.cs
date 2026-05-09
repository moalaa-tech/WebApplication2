using CRM.Domain.Base;
using CRM.Domain.Enums.ProjectManagment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Domain.Entities.SupplyChainManagement
{
    public class DemandPlanItem : BaseEntity
    {
        public int DemandPlanId { get; set; }
        public DemandPlan DemandPlan { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public decimal Quantity { get; set; }
        public string UnitOfMeasure { get; set; }
        public DateTime RequiredDate { get; set; }
        public CostType CostType { get; set; }
        public decimal EstimatedCost { get; set; }
        public int Priority { get; set; }
        public string Notes { get; set; }
        public bool IsProcured { get; set; }
    }
}

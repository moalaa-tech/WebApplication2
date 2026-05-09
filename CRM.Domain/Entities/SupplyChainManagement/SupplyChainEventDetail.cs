using CRM.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Domain.Entities.SupplyChainManagement
{
    public class SupplyChainEventDetail : BaseEntity
    {
        public SupplyChainEventDetail() { }
        public string DetailName { get; set; }
        public string DetailDescription { get; set; }
        public DateTime DetailDate { get; set; }
        public int SupplyChainEventId { get; set; }
        public SupplyChainEvent SupplyChainEvent { get; set; }
        public SupplyChainEventDetail(string detailName, string detailDescription, DateTime detailDate, int supplyChainEventId)
        {
            DetailName = detailName;
            DetailDescription = detailDescription;
            DetailDate = detailDate;
            SupplyChainEventId = supplyChainEventId;
        }
    }
}

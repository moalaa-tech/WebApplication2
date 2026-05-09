using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Domain.Integrations.SupplyChain
{
    public class CarrierTrackingResponse
    {
        public string Status { get; set; }
        public string CurrentStatus { get; set; }
        public DateTime? EstimatedDelivery { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public List<CarrierTrackingEvent> Events { get; set; }
    }
}

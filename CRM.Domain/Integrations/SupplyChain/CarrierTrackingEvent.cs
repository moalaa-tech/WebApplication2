using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Domain.Integrations.SupplyChain
{
    public class CarrierTrackingEvent
    {
        public DateTime Timestamp { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string EventCode { get; set; }
    }
}

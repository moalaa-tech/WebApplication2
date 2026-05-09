using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Domain.Integrations.SupplyChain
{
    public class CarrierRateResponse
    {
        public bool Success { get; set; }
        public decimal TotalCost { get; set; }
        public DateTime EstimatedDeliveryDate { get; set; }
        public string ServiceName { get; set; }
        public string TrackingNumber { get; set; }
        public string ErrorMessage { get; set; }
        public string CarrierServiceId { get; set; }
    }
}

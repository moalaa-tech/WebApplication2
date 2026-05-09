using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Domain.Integrations.SupplyChain
{
    public class CarrierRateApiResponse
    {
        public decimal TotalCharge { get; set; }
        public DateTime EstimatedDeliveryDate { get; set; }
        public string ServiceType { get; set; }
        public string ServiceCode { get; set; }
        public string TrackingNumber { get; set; }
    }
}

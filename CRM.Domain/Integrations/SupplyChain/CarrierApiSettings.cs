using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Domain.Integrations.SupplyChain
{
    public class CarrierApiSettings
    {
        public string BaseUrl { get; set; }
        public string ApiKey { get; set; }
        public string CarrierId { get; set; }
        public string CarrierName { get; set; }
    }
}

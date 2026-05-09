using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Domain.Integrations.SupplyChain
{
    public class CarrierLabelResponse
    {
        public string TrackingNumber { get; set; }
        public string LabelBase64 { get; set; }
        public string FileFormat { get; set; }
        public string LabelUrl { get; set; }
    }
}

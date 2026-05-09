using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Domain.Integrations.SupplyChain
{
    public class CarrierServiceOption
    {
        public string ServiceCode { get; set; }
        public string ServiceName { get; set; }
        public decimal Cost { get; set; }
        public int TransitDays { get; set; }
        public bool RequiresSignature { get; set; }
        public bool IncludesInsurance { get; set; }
        public decimal MaxInsuranceValue { get; set; }
    }
}

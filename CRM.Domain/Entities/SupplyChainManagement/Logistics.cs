using CRM.Domain.Base;
using CRM.Domain.Enums.SupplyChainManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Domain.Entities.SupplyChainManagement
{
    public class Logistics : BaseEntity
    {
        public string Name { get; set; }
        public LogisticsType Type { get; set; }
        public string ContactPerson { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
    }
}

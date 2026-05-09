using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Domain.Enums.SupplyChainManagement
{
    public enum ShippingStatus
    {
        Pending,
        InTransit,
        Delivered,
        Delayed,
        Cancelled,
        Returned
    }
}

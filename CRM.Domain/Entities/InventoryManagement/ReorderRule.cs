using CRM.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Domain.Entities.InventoryManagement
{
    public class ReorderRule : BaseEntity
    {
        public int ItemId { get; set; }
        public Product Item { get; set; }
        public decimal MinQty { get; set; }
        public decimal MaxQty { get; set; }
        public decimal ReorderQty { get; set; }
        public int MinimumQuantity { get; set; }
    }
}

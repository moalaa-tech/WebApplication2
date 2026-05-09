using CRM.Domain.Base;
using CRM.Domain.Enums.SupplyChainManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Domain.Entities.SupplyChainManagement
{
    public class SupplierCollaboration : BaseEntity
    {
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }
        public CollaborationType Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Notes { get; set; }
        public CollaborationStatus Status { get; set; }
    }
}

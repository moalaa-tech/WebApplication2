using CRM.Domain.Base;
using CRM.Domain.Enums.SupplyChainManagement;


namespace CRM.Domain.Entities.SupplyChainManagement
{
    public class Supplier : BaseEntity
    {
        public string Name { get; set; }
        public string NameAr { get; set; }
        public string Code { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        public SupplierStatus Status { get; set; }
        public DateTime ContractStartDate { get; set; }
        public DateTime? ContractEndDate { get; set; }
        public string Address { get; set; }
        public string ContactPerson { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        

        public ICollection<SupplierCollaboration> Collaborations { get; set; }
        public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; }

    }
}

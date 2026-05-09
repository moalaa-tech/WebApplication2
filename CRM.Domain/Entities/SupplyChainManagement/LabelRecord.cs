using CRM.Domain.Base;
using CRM.Domain.IdentityEntity;

namespace CRM.Domain.Entities.SupplyChainManagement
{
    public class LabelRecord : BaseEntity
    {
        public string TrackingNumber { get; set; }
        public string CarrierService { get; set; }
        public byte[] LabelData { get; set; }
        public string FileFormat { get; set; } = "PDF";
        public string LabelUrl { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public bool IsUsed { get; set; }
        public int CreatedById { get; set; }
        public virtual ApplicationUser CreatedBy { get; set; }
        public int ShipmentId { get; set; }
        public virtual Shipping Shipment { get; set; }
    }
}

using CRM.Domain.Base;

namespace CRM.Domain.Entities.InventoryManagement
{
    public class WarehouseZone : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string NameAR { get; set; } = string.Empty;
        public int WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; } = null!;
    }
}

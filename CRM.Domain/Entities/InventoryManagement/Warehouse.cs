using CRM.Domain.Base;
using CRM.Domain.IdentityEntity;

namespace CRM.Domain.Entities.InventoryManagement
{
    public class Warehouse : BaseEntity
    {
        public required string Name { get; set; }
        public required string NameAr { get; set; }
        public string Location { get; set; }
        public int CityId { get; set; }
        public required City City { get; set; }
        public int WarehouseTypeId { get; set; }
        public required WarehouseType WarehouseType { get; set; }
        public int Capacity { get; set; }

        public int ManagerId { get; set; }
        public required ApplicationUser Manager { get; set; }

        public ICollection<StockTransaction> StockTransactions { get; set; }

        public ICollection<WarehouseZone> Zones { get; set; } = new List<WarehouseZone>();
    }
}

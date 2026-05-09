using CRM.Domain.Base;

namespace CRM.Domain.Entities.InventoryManagement
{
    public class Purchase : BaseEntity
    {
        public string Name { get; set; }
        public string NameAr { get; set; }
        public int Count { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Tax { get; set; }
        public string Supplier { get; set; }
        public DateTime OrderDate { get; set; }
        public ICollection<PurchaseOrderLine> Lines { get; set; }

    }
}

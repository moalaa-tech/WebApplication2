using CRM.Domain.Base;

namespace CRM.Domain.Entities.InventoryManagement
{
    public class ProductType : BaseEntity
    {
        public string Name { get; set; }
        public string NameAr { get; set; }
        public string Description { get; set; }


        // Navigation property for a one-to-many relationship with Products
        public ICollection<Product> Products { get; set; } = new List<Product>();

    }
}

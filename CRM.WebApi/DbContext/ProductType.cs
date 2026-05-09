

namespace CRM.WebApi.DbContext
{
    public class ProductType : BaseEntity
    {
        public string Name { get; set; }
        public string NameAR { get; set; }

        public string Description { get; set; }


        // Navigation property for a one-to-many relationship with Products
        public ICollection<Product> Products { get; set; } = new List<Product>();

    }
}



using CRM.WebApi.DbContext.EasyOrderModels;

namespace CRM.WebApi.DbContext
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string NameAr { get; set; }
        public decimal TotalCost { get; set; }
        public decimal ShippingCost { get; set; }
        public decimal FreightCost { get; set; }
        public int? ProductTypeId { get; set; }
        public ProductType? ProductType { get; set; }
        public string? ImageFile { get; set; }
        public string? Description { get; set; }
        public int QuantityInStock { get; set; }

        public Guid? EasyOrderProductId { get; set; }
        public EasyOrderProduct? EasyOrderProduct { get; set; }

        public ICollection<ProductVariant>? ProductVariants { get; set; }

    }
}

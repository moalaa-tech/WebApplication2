

namespace CRM.WebApi.DbContext
{
    public class ProductVariant : BaseEntity
    {
        public Guid VariantId { get; set; }
        public int Quantity { get; set; }


        public string? Variation { get; set; }
        public string? VariationAr { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }

    }
}

namespace CRM.WebApi.EasyOrder
{
    public class VariantDto
    {
        public Guid? id { get; set; }
        public Guid? product_id { get; set; }
        public decimal? price { get; set; }
        public decimal? sale_price { get; set; }
        public int? quantity { get; set; }
        public string? taager_code { get; set; }
        public List<VariationPropDto>? variation_props { get; set; }
    }
}

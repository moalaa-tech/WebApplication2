namespace CRM.WebApi.EasyOrder
{
    public class VariationPropDto
    {
        public Guid? id { get; set; }
        public string? variation { get; set; }
        public string? variation_prop { get; set; }
        public Guid? product_variant_id { get; set; }
    }
}

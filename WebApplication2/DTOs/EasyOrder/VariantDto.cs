namespace CRM.WebApp.DTOs.EasyOrder
{
    public class VariantDto
    {
        public string id { get; set; }
        public string product_id { get; set; }
        public int price { get; set; }
        public int sale_price { get; set; }
        public int quantity { get; set; }
        public string taager_code { get; set; }
        public List<VariationPropDto> variation_props { get; set; }
    }
}

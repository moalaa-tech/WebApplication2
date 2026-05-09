namespace CRM.WebApp.DTOs.EasyOrder
{
    public class CartItemDto
    {
        public string id { get; set; }
        public string product_id { get; set; }
        public string variant_id { get; set; }
        public string store_id { get; set; }
        public int price { get; set; }
        public int quantity { get; set; }
        public EasyOrderProductDto product { get; set; }
        public VariantDto variant { get; set; }
        public string order_id { get; set; }
    }
}

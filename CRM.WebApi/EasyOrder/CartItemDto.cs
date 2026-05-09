namespace CRM.WebApi.EasyOrder
{
    public class CartItemDto
    {
        public Guid? id { get; set; }
        public DateTime updated_at { get; set; }
        public DateTime created_at { get; set; }
        public Guid? guest_id { get; set; }

        public Guid? product_id { get; set; }
        public Guid? variant_id { get; set; }
        public Guid? store_id { get; set; }
        public decimal? price { get; set; }
        public int? quantity { get; set; }
        public bool? in_cart { get; set; }
        public bool? is_upsell { get; set; }
        public Guid? order_id { get; set; }

        public EasyOrderProductDto? product { get; set; }
        public VariantDto? variant { get; set; }

        
    }
}

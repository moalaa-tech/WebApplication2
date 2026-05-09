namespace CRM.WebApp.DTOs.EasyOrder
{
    public class EasyOrderDto
    {
        public string id { get; set; }
        public DateTime updated_at { get; set; }
        public DateTime created_at { get; set; }
        public string store_id { get; set; }
        public int cost { get; set; }
        public int shipping_cost { get; set; }
        public int total_cost { get; set; }
        public string status { get; set; }
        public string full_name { get; set; }
        public string phone { get; set; }
        public string government { get; set; }
        public string address { get; set; }
        public string payment_method { get; set; }
        public List<CartItemDto> cart_items { get; set; }
    }
}

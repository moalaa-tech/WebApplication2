namespace CRM.WebApp.DTOs.EasyOrder
{
    public class EasyOrderProductDto
    {
        public string id { get; set; }
        public DateTime updated_at { get; set; }
        public DateTime created_at { get; set; }
        public string store_id { get; set; }
        public string name { get; set; }
        public int price { get; set; }
        public string sku { get; set; }
        public string taager_code { get; set; }
        public string drop_shipping_provider { get; set; }
    }
}

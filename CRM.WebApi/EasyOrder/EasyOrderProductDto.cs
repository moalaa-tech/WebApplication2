namespace CRM.WebApi.EasyOrder
{
    public class EasyOrderProductDto
    {
        public Guid? id { get; set; }
        public DateTime? updated_at { get; set; }
        public DateTime? created_at { get; set; }
        public Guid? store_id { get; set; }
        public string? name { get; set; }
        public decimal? price { get; set; }
        public decimal? sale_price { get; set; }
        public string? description { get; set; }
        public string? slug { get; set; }
        public string? thumb { get; set; }
        public List<string>? images { get; set; }
        public int? position { get; set; }
        public bool? hidden { get; set; }
        public int? quantity { get; set; }
        public bool? track_stock { get; set; }
        public bool? disable_orders_for_no_stock { get; set; }
        public string? meta_description { get; set; }
        public bool? show_landing_in_same_page { get; set; }
        public bool? is_skip_cart { get; set; }
        public string? buy_now_text { get; set; }
        public bool? is_fixed_bottom_buy { get; set; }
        public bool? is_one_page_checkout { get; set; }
        public bool? is_fake_visitors { get; set; }
        public int? fake_visitors_min { get; set; }
        public int? fake_visitors_max { get; set; }
        public bool? is_fake_stock { get; set; }
        public bool? is_fake_timer { get; set; }
        public int? fake_timer_hours { get; set; }
        public bool? is_quantity_hidden { get; set; }
        public bool? is_header_hidden { get; set; }
        public bool? is_free_shipping { get; set; }
        public string? custom_currency { get; set; }
        public bool? is_checkout_before_description { get; set; }
        public bool? hide_related_products { get; set; }
        public bool? is_taager_submit_active { get; set; }
        public bool? is_ecombo_submit_active { get; set; }
        public bool? is_mosaweq_submit_active { get; set; }
        public bool? is_alturky_submit_active { get; set; }
        public bool? is_jamaica_submit_active { get; set; }
        public bool? is_engzny_submit_active { get; set; }
        public bool? is_digital { get; set; }
        public bool? is_cloaking_active { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Domain.Entities.MarketingAutomation.EasyOrder
{
    public class EasyOrderRequest
    {
        public Guid? id { get; set; }
        public DateTime? updated_at { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? created_day { get; set; }

        public Guid? store_id { get; set; }
        public decimal? cost { get; set; }
        public decimal? shipping_cost { get; set; }
        public decimal? total_cost { get; set; }
        public string? status { get; set; }
        public int? short_id { get; set; }

        public string? full_name { get; set; }
        public string? phone { get; set; }
        public string? government { get; set; }
        public string? address { get; set; }
        public string? payment_method { get; set; }
        public string? utm_source { get; set; }
        public string? utm_campaign { get; set; }
        public string? ip { get; set; }
        public string? ip_country { get; set; }
        public string? note { get; set; }
        public List<EasyOrderCartItem>? cart_items { get; set; }
    }
}

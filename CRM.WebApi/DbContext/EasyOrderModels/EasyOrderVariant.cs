using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.WebApi.DbContext.EasyOrderModels
{
    public class EasyOrderVariant
    {
        public Guid? id { get; set; }
        public Guid? product_id { get; set; }
        public EasyOrderProduct? product { get; set; }
        public decimal? price { get; set; }
        public decimal? sale_price { get; set; }
        public int? quantity { get; set; }
        public string? taager_code { get; set; }
        public List<EasyOrderVariationProp>? variation_props { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.WebApi.DbContext.EasyOrderModels
{
    public class EasyOrderVariationProp
    {
        public Guid? id { get; set; }
        public string? variation { get; set; }
        public string? variation_prop { get; set; }
        public Guid? product_variant_id { get; set; }
        public EasyOrderVariant? product_variant { get; set; }
    }
}

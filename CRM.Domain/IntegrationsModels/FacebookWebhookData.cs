using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CRM.Domain.IntegrationsModels
{
    public class FacebookWebhookData
    {
        [JsonPropertyName("object")]
        public string Object { get; set; }

        [JsonPropertyName("entry")]
        public List<FacebookWebhookEntry> Entry { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CRM.Domain.IntegrationsModels.Instagram
{
    public class InstagramWebhookEntry
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("time")]
        public long Time { get; set; }

        [JsonPropertyName("changes")]
        public List<InstagramChange> Changes { get; set; }
    }
}

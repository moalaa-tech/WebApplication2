using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CRM.Domain.IntegrationsModels.Instagram
{
    public class InstagramPublishResponse
    {
        [JsonPropertyName("id")]
        public string ContainerId { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("status_code")]
        public string StatusCode { get; set; }
    }
}

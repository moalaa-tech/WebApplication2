using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CRM.Domain.IntegrationsModels.Instagram
{
    public class InstagramMediaResponse
    {
        [JsonPropertyName("id")]
        public string MediaId { get; set; }
    }
}

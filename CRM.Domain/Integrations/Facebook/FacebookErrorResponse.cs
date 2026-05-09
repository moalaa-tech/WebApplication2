using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CRM.Domain.Integrations.Facebook
{
    public class FacebookErrorResponse
    {
        [JsonPropertyName("error")]
        public FacebookError Error { get; set; }
    }
}

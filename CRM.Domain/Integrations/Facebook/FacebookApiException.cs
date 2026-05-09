using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Domain.Integrations.Facebook
{
    public class FacebookApiException : Exception
    {
        public FacebookError ErrorDetails { get; }

        public FacebookApiException(string message, FacebookError errorDetails = null)
            : base(message)
        {
            ErrorDetails = errorDetails;
        }
    }
}

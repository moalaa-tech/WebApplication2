using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Domain.Integrations.Facebook
{
    // Services/Interfaces/IFacebookApiClient.cs
    public interface IFacebookApiClient : ISocialMediaApiClient
    {
        Task<string> GetLongLivedAccessTokenAsync(string shortLivedToken);
        Task<FacebookPostResponse> PublishToPageAsync(string pageId, string message);
        Task<FacebookPostResponse> PublishToUserFeedAsync(string message);
        Task SchedulePostAsync(string pageId, string message, DateTime scheduledPublishTime);
    }
}

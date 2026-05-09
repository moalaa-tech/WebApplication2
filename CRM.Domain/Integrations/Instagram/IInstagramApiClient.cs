using CRM.Domain.IntegrationsModels.Instagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Domain.Integrations.Instagram
{
    // Services/Interfaces/IInstagramApiClient.cs
    public interface IInstagramApiClient : ISocialMediaApiClient
    {
        Task<string> GetLongLivedAccessTokenAsync(string shortLivedToken);
        Task<string> GetUserIdAsync();
        Task<InstagramMediaResponse> UploadImageAsync(Stream imageStream, string caption);
        Task<InstagramMediaResponse> UploadVideoAsync(Stream videoStream, string caption);
        Task<InstagramPublishResponse> PublishMediaAsync(string containerId);
        Task SchedulePostAsync(Stream mediaStream, string caption, DateTime scheduledTime, InstagramMediaType mediaType);
    }
}

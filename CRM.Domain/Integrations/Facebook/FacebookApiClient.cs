using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using System.Text.Json;

namespace CRM.Domain.Integrations.Facebook
{
    public class FacebookApiClient : IFacebookApiClient
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;
        private readonly ILogger<FacebookApiClient> _logger;
        private readonly IMemoryCache _cache;

        public FacebookApiClient(
            IHttpClientFactory clientFactory,
            IConfiguration configuration,
            ILogger<FacebookApiClient> logger,
            IMemoryCache cache)
        {
            _clientFactory = clientFactory;
            _configuration = configuration;
            _logger = logger;
            _cache = cache;
        }

        public async Task<bool> PublishPostAsync(string content)
        {
            try
            {
                var defaultPageId = _configuration["SocialMedia:Facebook:DefaultPageId"];
                var response = await PublishToPageAsync(defaultPageId, content);
                return !string.IsNullOrEmpty(response?.PostId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to publish post to Facebook");
                return false;
            }
        }

        public async Task<string> GetLongLivedAccessTokenAsync(string shortLivedToken)
        {
            var client = _clientFactory.CreateClient("Facebook");

            var appId = _configuration["SocialMedia:Facebook:AppId"];
            var appSecret = _configuration["SocialMedia:Facebook:AppSecret"];

            var response = await client.GetFromJsonAsync<FacebookAuthResponse>(
                $"oauth/access_token?grant_type=fb_exchange_token" +
                $"&client_id={appId}" +
                $"&client_secret={appSecret}" +
                $"&fb_exchange_token={shortLivedToken}");

            return response?.AccessToken;
        }

        public async Task<FacebookPostResponse> PublishToPageAsync(string pageId, string message)
        {
            if (string.IsNullOrEmpty(pageId))
                throw new ArgumentException("Page ID is required");

            var accessToken = await GetAccessTokenAsync();

            var client = _clientFactory.CreateClient("Facebook");
            var response = await client.PostAsJsonAsync(
                $"{pageId}/feed?access_token={accessToken}",
                new { message });

            return await HandleFacebookResponse<FacebookPostResponse>(response);
        }

        public async Task<FacebookPostResponse> PublishToUserFeedAsync(string message)
        {
            var accessToken = await GetAccessTokenAsync();

            var client = _clientFactory.CreateClient("Facebook");
            var response = await client.PostAsJsonAsync(
                $"me/feed?access_token={accessToken}",
                new { message });

            return await HandleFacebookResponse<FacebookPostResponse>(response);
        }

        public async Task SchedulePostAsync(string pageId, string message, DateTime scheduledPublishTime)
        {
            if (string.IsNullOrEmpty(pageId))
                throw new ArgumentException("Page ID is required");

            var accessToken = await GetAccessTokenAsync();

            // Facebook requires UTC time in ISO 8601 format
            var publishTime = scheduledPublishTime.ToUniversalTime()
                .ToString("yyyy-MM-ddTHH:mm:sszzz");

            var client = _clientFactory.CreateClient("Facebook");
            var response = await client.PostAsJsonAsync(
                $"{pageId}/feed?access_token={accessToken}",
                new
                {
                    message,
                    published = false,
                    scheduled_publish_time = publishTime
                });

            await HandleFacebookResponse<FacebookPostResponse>(response);
        }

        private async Task<string> GetAccessTokenAsync()
        {
            const string cacheKey = "FacebookAccessToken";

            if (_cache.TryGetValue(cacheKey, out string cachedToken))
            {
                return cachedToken;
            }

            // Get from configuration (for demo) - in production, get from database
            var accessToken = _configuration["SocialMedia:Facebook:AccessToken"];

            if (string.IsNullOrEmpty(accessToken))
            {
                throw new InvalidOperationException("Facebook access token is not configured");
            }

            // Cache the token for 1 hour (Facebook tokens typically last 2 hours)
            _cache.Set(cacheKey, accessToken, TimeSpan.FromHours(1));

            return accessToken;
        }

        private async Task<T> HandleFacebookResponse<T>(HttpResponseMessage response)
            where T : class
        {
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                var errorResponse = JsonSerializer.Deserialize<FacebookErrorResponse>(content);
                throw new FacebookApiException(
                    $"Facebook API error: {errorResponse?.Error?.Message ?? "Unknown error"}",
                    errorResponse?.Error);
            }

            return JsonSerializer.Deserialize<T>(content);
        }
    }

    
}

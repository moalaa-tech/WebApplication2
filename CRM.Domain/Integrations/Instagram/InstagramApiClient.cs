using CRM.Domain.IntegrationsModels.Instagram;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace CRM.Domain.Integrations.Instagram
{
    public class InstagramApiClient : IInstagramApiClient
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;
        private readonly ILogger<InstagramApiClient> _logger;
        private readonly IMemoryCache _cache;
        private readonly string _pageId;

        public InstagramApiClient(
            IHttpClientFactory clientFactory,
            IConfiguration configuration,
            ILogger<InstagramApiClient> logger,
            IMemoryCache cache)
        {
            _clientFactory = clientFactory;
            _configuration = configuration;
            _logger = logger;
            _cache = cache;
            _pageId = _configuration["SocialMedia:Instagram:PageId"];
        }

        public async Task<bool> PublishPostAsync(string content)
        {
            try
            {
                // For Instagram, we need an image with caption
                // This is a simplified version - in reality you'd need an image
                var tempImage = GenerateTempImage(content);
                using var stream = new MemoryStream(tempImage);

                var uploadResponse = await UploadImageAsync(stream, content);
                var publishResponse = await PublishMediaAsync(uploadResponse.MediaId);

                return publishResponse.Status == "FINISHED";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to publish post to Instagram");
                return false;
            }
        }

        public async Task<string> GetLongLivedAccessTokenAsync(string shortLivedToken)
        {
            var client = _clientFactory.CreateClient("Facebook");

            var response = await client.GetFromJsonAsync<InstagramAuthResponse>(
                $"oauth/access_token?grant_type=fb_exchange_token" +
                $"&client_id={_configuration["SocialMedia:Facebook:AppId"]}" +
                $"&client_secret={_configuration["SocialMedia:Facebook:AppSecret"]}" +
                $"&fb_exchange_token={shortLivedToken}");

            return response?.AccessToken;
        }

        public async Task<string> GetUserIdAsync()
        {
            var accessToken = await GetAccessTokenAsync();
            var client = _clientFactory.CreateClient("Facebook");

            var response = await client.GetAsync($"me/accounts?access_token={accessToken}");
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new InstagramApiException("Failed to get user pages", content);
            }

            // Parse response to find Instagram page
            // This is simplified - actual implementation would need to find the Instagram page
            return _pageId;
        }

        public async Task<InstagramMediaResponse> UploadImageAsync(Stream imageStream, string caption)
        {
            var accessToken = await GetAccessTokenAsync();
            var client = _clientFactory.CreateClient("Facebook");

            using var content = new MultipartFormDataContent();
            content.Add(new StreamContent(imageStream), "file", "image.jpg");
            content.Add(new StringContent(caption), "caption");

            var response = await client.PostAsync(
                $"{_pageId}/media?access_token={accessToken}&image_type=UPLOAD", content);

            return await HandleInstagramResponse<InstagramMediaResponse>(response);
        }

        public async Task<InstagramMediaResponse> UploadVideoAsync(Stream videoStream, string caption)
        {
            var accessToken = await GetAccessTokenAsync();
            var client = _clientFactory.CreateClient("Facebook");

            using var content = new MultipartFormDataContent();
            content.Add(new StreamContent(videoStream), "file", "video.mp4");
            content.Add(new StringContent(caption), "caption");

            var response = await client.PostAsync(
                $"{_pageId}/media?access_token={accessToken}&media_type=VIDEO", content);

            return await HandleInstagramResponse<InstagramMediaResponse>(response);
        }

        public async Task<InstagramPublishResponse> PublishMediaAsync(string containerId)
        {
            var accessToken = await GetAccessTokenAsync();
            var client = _clientFactory.CreateClient("Facebook");

            var response = await client.PostAsync(
                $"{_pageId}/media_publish?access_token={accessToken}&creation_id={containerId}",
                null);

            return await HandleInstagramResponse<InstagramPublishResponse>(response);
        }

        public async Task SchedulePostAsync(Stream mediaStream, string caption, DateTime scheduledTime, InstagramMediaType mediaType)
        {
            var accessToken = await GetAccessTokenAsync();
            var client = _clientFactory.CreateClient("Facebook");

            // Instagram scheduling is done through Facebook's API
            var publishTime = scheduledTime.ToUniversalTime()
                .ToString("yyyy-MM-ddTHH:mm:sszzz");

            using var content = new MultipartFormDataContent();

            if (mediaType == InstagramMediaType.IMAGE)
            {
                content.Add(new StreamContent(mediaStream), "file", "image.jpg");
                content.Add(new StringContent("IMAGE"), "media_type");
            }
            else
            {
                content.Add(new StreamContent(mediaStream), "file", "video.mp4");
                content.Add(new StringContent("VIDEO"), "media_type");
            }

            content.Add(new StringContent(caption), "caption");
            content.Add(new StringContent("false"), "published");
            content.Add(new StringContent(publishTime), "scheduled_publish_time");

            var response = await client.PostAsync(
                $"{_pageId}/media?access_token={accessToken}", content);

            await HandleInstagramResponse<InstagramMediaResponse>(response);
        }

        private async Task<string> GetAccessTokenAsync()
        {
            const string cacheKey = "InstagramAccessToken";

            if (_cache.TryGetValue(cacheKey, out string cachedToken))
            {
                return cachedToken;
            }

            // Get from configuration (for demo) - in production, get from database
            var accessToken = _configuration["SocialMedia:Instagram:AccessToken"];

            if (string.IsNullOrEmpty(accessToken))
            {
                throw new InvalidOperationException("Instagram access token is not configured");
            }

            // Cache the token for 1 hour
            _cache.Set(cacheKey, accessToken, TimeSpan.FromHours(1));

            return accessToken;
        }

        private async Task<T> HandleInstagramResponse<T>(HttpResponseMessage response)
            where T : class
        {
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                var errorResponse = JsonSerializer.Deserialize<InstagramErrorResponse>(content);
                throw new InstagramApiException(
                    $"Instagram API error: {errorResponse?.Error?.Message ?? "Unknown error"}",
                    errorResponse?.Error);
            }

            return JsonSerializer.Deserialize<T>(content);
        }

        private byte[] GenerateTempImage(string text)
        {
            // This is just for demo purposes - in a real app you'd use a real image
            //using var image = new Bitmap(800, 800);
            //using var graphics = Graphics.FromImage(image);
            //graphics.Clear(Color.White);

            //var font = new Font("Arial", 40);
            //var brush = new SolidBrush(Color.Black);
            //graphics.DrawString(text, font, brush, new PointF(50, 350));

            using var ms = new MemoryStream();
            //image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return ms.ToArray();
        }
    }

    public class InstagramApiException : Exception
    {
        public InstagramError ErrorDetails { get; }

        public InstagramApiException(string message, InstagramError errorDetails = null)
            : base(message)
        {
            ErrorDetails = errorDetails;
        }

        public InstagramApiException(string message, string responseContent)
            : base($"{message}. Response: {responseContent}")
        {
        }
    }
}

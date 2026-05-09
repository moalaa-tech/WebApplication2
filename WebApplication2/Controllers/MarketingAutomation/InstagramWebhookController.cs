using CRM.Domain.Integrations.Instagram;
using CRM.Domain.IntegrationsModels.Instagram;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace CRM.WebApp.Controllers.MarketingAutomation
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstagramWebhookController : ControllerBase
    {
        private readonly ILogger<InstagramWebhookController> _logger;
        private readonly IInstagramApiClient _instagramClient;
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _clientFactory;



        public InstagramWebhookController(
                    IHttpClientFactory clientFactory,
            ILogger<InstagramWebhookController> logger, 
            IInstagramApiClient instagramClient,
        IConfiguration configuration
        )
        {
            _logger = logger;
            _instagramClient = instagramClient;
            _configuration = configuration;
            _clientFactory = clientFactory;
        }


        [HttpGet("login")]
        public IActionResult Login()
        {
            var appId = _configuration["SocialMedia:Facebook:AppId"];
            var redirectUri = Uri.EscapeDataString("https://yourdomain.com/api/instagramauth/callback");
            var scopes = "instagram_basic,instagram_content_publish,pages_show_list,pages_read_engagement";

            var authUrl = $"https://www.facebook.com/v15.0/dialog/oauth?" +
                          $"client_id={appId}" +
                          $"&redirect_uri={redirectUri}" +
                          $"&scope={scopes}" +
                          $"&response_type=code";

            return Redirect(authUrl);
        }

        [HttpGet("callback")]
        public async Task<IActionResult> Callback([FromQuery] string code)
        {
            try
            {
                var client = _clientFactory.CreateClient("Facebook");

                var appId = _configuration["SocialMedia:Facebook:AppId"];
                var appSecret = _configuration["SocialMedia:Facebook:AppSecret"];
                var redirectUri = "https://yourdomain.com/api/instagramauth/callback";

                var response = await client.GetFromJsonAsync<InstagramAuthResponse>(
                    $"oauth/access_token?" +
                    $"client_id={appId}" +
                    $"&client_secret={appSecret}" +
                    $"&redirect_uri={redirectUri}" +
                    $"&code={code}");

                // Exchange for long-lived token
                var longLivedToken = await _instagramClient.GetLongLivedAccessTokenAsync(response.AccessToken);

                // Store this token securely in your database
                // ...

                return Ok("Instagram authentication successful!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Instagram authentication failed");
                return BadRequest("Authentication failed");
            }
        }


        [HttpGet]
        public IActionResult VerifyWebhook(
            [FromQuery(Name = "hub.mode")] string mode,
            [FromQuery(Name = "hub.challenge")] string challenge,
            [FromQuery(Name = "hub.verify_token")] string verifyToken)
        {
            var expectedToken = "YOUR_VERIFY_TOKEN";

            if (mode == "subscribe" && verifyToken == expectedToken)
            {
                _logger.LogInformation("Instagram webhook verified");
                return Ok(challenge);
            }

            _logger.LogWarning("Instagram webhook verification failed");
            return Forbid();
        }

        [HttpPost]
        public async Task<IActionResult> HandleWebhook([FromBody] InstagramWebhookData data)
        {
            _logger.LogInformation("Received Instagram webhook data");

            foreach (var entry in data.Entry)
            {
                foreach (var change in entry.Changes)
                {
                    _logger.LogInformation($"Instagram change received: {change.Field}");

                    switch (change.Field)
                    {
                        case "comments":
                            await HandleCommentChange(change);
                            break;
                        case "mentions":
                            await HandleMentionChange(change);
                            break;
                    }
                }
            }

            return Ok();
        }

        private async Task HandleCommentChange(InstagramChange change)
        {
            // Example: { "field": "comments", "value": { "media_id": "17841405783087214", "comment_id": "17851087208098166" } }
            var mediaId = change.Value?.media_id?.ToString();
            var commentId = change.Value?.comment_id?.ToString();

            _logger.LogInformation($"New comment on media {mediaId}: {commentId}");
        }

        private async Task HandleMentionChange(InstagramChange change)
        {
            // Example: { "field": "mentions", "value": { "media_id": "17841405783087214", "comment_id": "17851087208098166" } }
            var mediaId = change.Value?.media_id?.ToString();
            var commentId = change.Value?.comment_id?.ToString();

            _logger.LogInformation($"New mention on media {mediaId}: {commentId}");
        }
    }
}

using CRM.Domain.IntegrationsModels;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace CRM.WebApp.Controllers.MarketingAutomation
{
    // Controllers/FacebookWebhookController.cs
    [Route("api/[controller]")]
    [ApiController]
    public class FacebookWebhookController : ControllerBase
    {
        private readonly ILogger<FacebookWebhookController> _logger;

        public FacebookWebhookController(ILogger<FacebookWebhookController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult VerifyWebhook(
            [FromQuery(Name = "hub.mode")] string mode,
            [FromQuery(Name = "hub.challenge")] string challenge,
            [FromQuery(Name = "hub.verify_token")] string verifyToken)
        {
            var expectedToken = "YOUR_VERIFY_TOKEN"; // Should come from config

            if (mode == "subscribe" && verifyToken == expectedToken)
            {
                _logger.LogInformation("Webhook verified");
                return Ok(challenge);
            }

            _logger.LogWarning("Webhook verification failed");
            return Forbid();
        }

        [HttpPost]
        public async Task<IActionResult> HandleWebhook([FromBody] FacebookWebhookData data)
        {
            _logger.LogInformation("Received Facebook webhook data");

            // Process different webhook events
            foreach (var entry in data.Entry)
            {
                foreach (var change in entry.Changes)
                {
                    _logger.LogInformation($"Change received for {change.Field}");

                    // Handle different types of changes
                    switch (change.Field)
                    {
                        case "feed":
                            await HandleFeedChange(change);
                            break;
                            // Add other cases as needed
                    }
                }
            }

            return Ok();
        }

        private async Task HandleFeedChange(FacebookChange change)
        {
            // Implement your feed change handling logic
            _logger.LogInformation($"Feed change: {change.Value}");
        }
    }
}

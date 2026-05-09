using Microsoft.Extensions.Caching.Memory;
using System.Net;
using System.Net.Sockets;
using CRM.WebApp.Security;

namespace CRM.WebApp.Middlewares
{
    public class RateLimitingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IMemoryCache _cache;
        private readonly ILogger<RateLimitingMiddleware> _logger;

        // Rate limiting configuration
        private readonly Dictionary<string, RateLimitRule> _rules = new()
        {
            { "/Authentication/Login", new RateLimitRule { MaxRequests = 5, TimeWindow = TimeSpan.FromMinutes(15) } },
            { "/Authentication/Register", new RateLimitRule { MaxRequests = 3, TimeWindow = TimeSpan.FromHours(1) } },
            { "/api/", new RateLimitRule { MaxRequests = 100, TimeWindow = TimeSpan.FromMinutes(1) } }, // For API endpoints
            { "default", new RateLimitRule { MaxRequests = 200, TimeWindow = TimeSpan.FromMinutes(1) } }
        };

        public RateLimitingMiddleware(RequestDelegate next, 
            IMemoryCache cache, 
            ILogger<RateLimitingMiddleware> logger)
        {
            _next = next;
            _cache = cache;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var clientId = GetClientIdentifier(context);
            var path = context.Request.Path.Value?.ToLower() ?? "";

            // Find applicable rate limit rule
            var rule = _rules.FirstOrDefault(r => path.StartsWith(r.Key.ToLower())).Value ?? _rules["default"];

            var key = $"rate_limit_{clientId}_{GetRuleKey(path)}";
            var requestCount = _cache.GetOrCreate(key, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = rule.TimeWindow;
                return 0;
            });

            requestCount++;
            _cache.Set(key, requestCount, rule.TimeWindow);

            if (requestCount > rule.MaxRequests)
            {
                await HandleRateLimitExceeded(context, clientId, path, requestCount, rule);
                return;
            }

            // Add rate limit headers (check if they don't already exist)
            if (!context.Response.Headers.ContainsKey("X-RateLimit-Limit"))
                context.Response.Headers.Add("X-RateLimit-Limit", rule.MaxRequests.ToString());
            if (!context.Response.Headers.ContainsKey("X-RateLimit-Remaining"))
                context.Response.Headers.Add("X-RateLimit-Remaining", Math.Max(0, rule.MaxRequests - requestCount).ToString());
            if (!context.Response.Headers.ContainsKey("X-RateLimit-Reset"))
                context.Response.Headers.Add("X-RateLimit-Reset", DateTimeOffset.UtcNow.Add(rule.TimeWindow).ToUnixTimeSeconds().ToString());

            await _next(context);
        }

        private async Task HandleRateLimitExceeded(HttpContext context, string clientId, string path, int requestCount, RateLimitRule rule)
        {
            _logger.LogWarning("Rate limit exceeded for client {ClientId} on path {Path}. Requests: {RequestCount}, Limit: {Limit}",
                clientId, path, requestCount, rule.MaxRequests);

            try
            {
                var securityAuditService = context.RequestServices.GetService<ISecurityAuditService>();
                if (securityAuditService != null)
                {
                    await securityAuditService.LogSuspiciousActivityAsync(
                        "Rate Limit Exceeded",
                        $"Client {clientId} exceeded rate limit on {path}. Requests: {requestCount}/{rule.MaxRequests}",
                        SuspiciousActivityLevel.Medium);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to log rate limit violation to audit service");
            }

            context.Response.StatusCode = (int)HttpStatusCode.TooManyRequests;
            context.Response.ContentType = "application/json";

            var response = new
            {
                error = "Rate limit exceeded",
                message = $"Too many requests. Limit: {rule.MaxRequests} per {rule.TimeWindow.TotalMinutes} minutes",
                retryAfter = rule.TimeWindow.TotalSeconds
            };

            await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(response));
        }

        private string GetClientIdentifier(HttpContext context)
        {
            // Use user ID if authenticated, otherwise use IP address
            var userId = context.User?.Identity?.Name;
            if (!string.IsNullOrEmpty(userId))
            {
                return $"user_{userId}";
            }

            // Get client IP address
            var forwardedFor = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (!string.IsNullOrEmpty(forwardedFor))
            {
                return $"ip_{forwardedFor.Split(',')[0].Trim()}";
            }

            return $"ip_{context.Connection.RemoteIpAddress}";
        }

        private string GetRuleKey(string path)
        {
            // Find the most specific rule key for the path
            return _rules.Keys.Where(k => k != "default" && path.StartsWith(k.ToLower()))
                             .OrderByDescending(k => k.Length)
                             .FirstOrDefault() ?? "default";
        }
    }

    public class RateLimitRule
    {
        public int MaxRequests { get; set; }
        public TimeSpan TimeWindow { get; set; }
    }

    // URL validation service for SSRF prevention
    public interface IUrlValidationService
    {
        bool IsUrlSafe(string url);
        bool IsInternalUrl(string url);
        Task<bool> ValidateExternalRequestAsync(string url);
    }

    public class UrlValidationService : IUrlValidationService
    {
        private readonly ILogger<UrlValidationService> _logger;

        // Whitelist of allowed external domains
        private readonly HashSet<string> _allowedDomains = new()
        {
            "api.github.com",
            "www.googleapis.com",
            "graph.microsoft.com"
            // Add your trusted external domains here
        };

        // Blacklisted IP ranges (internal networks)
        private readonly string[] _blacklistedIpRanges = {
            "127.0.0.0/8",    // Loopback
            "10.0.0.0/8",     // Private networks
            "172.16.0.0/12",  // Private networks
            "192.168.0.0/16", // Private networks
            "169.254.0.0/16", // Link-local
            "0.0.0.0/8",      // Invalid
            "224.0.0.0/4"     // Multicast
        };

        public UrlValidationService(ILogger<UrlValidationService> logger)
        {
            _logger = logger;
        }

        public bool IsUrlSafe(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return false;

            if (!Uri.TryCreate(url, UriKind.Absolute, out Uri uri))
                return false;

            // Only allow HTTP and HTTPS
            if (uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps)
            {
                LogSuspiciousUrl(url, "Invalid URL scheme");
                return false;
            }

            // Check if it's an internal URL
            if (IsInternalUrl(url))
            {
                LogSuspiciousUrl(url, "Internal URL access attempt");
                return false;
            }

            // Check if domain is in whitelist
            if (!_allowedDomains.Contains(uri.Host.ToLower()))
            {
                LogSuspiciousUrl(url, "Domain not in whitelist");
                return false;
            }

            return true;
        }

        public bool IsInternalUrl(string url)
        {
            if (!Uri.TryCreate(url, UriKind.Absolute, out Uri uri))
                return true; // Assume dangerous if we can't parse

            var host = uri.Host;
            
            // Check if it's localhost
            if (host.Equals("localhost", StringComparison.OrdinalIgnoreCase) ||
                host.Equals("127.0.0.1") ||
                host.Equals("::1"))
            {
                return true;
            }

            // Check if it's a private IP address
            if (IPAddress.TryParse(host, out IPAddress ipAddress))
            {
                return IsPrivateIP(ipAddress);
            }

            return false;
        }

        public async Task<bool> ValidateExternalRequestAsync(string url)
        {
            if (!IsUrlSafe(url))
                return false;

            try
            {
                // Additional validation could include DNS resolution check
                // to ensure the domain doesn't resolve to internal IPs
                var hostEntry = await Dns.GetHostEntryAsync(new Uri(url).Host);
                foreach (var address in hostEntry.AddressList)
                {
                    if (IsPrivateIP(address))
                    {
                        LogSuspiciousUrl(url, "Domain resolves to private IP");
                        return false;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Error validating external URL: {Url}", url);
                return false;
            }
        }

        private bool IsPrivateIP(IPAddress ipAddress)
        {
            var bytes = ipAddress.GetAddressBytes();
            
            // IPv4 checks
            if (ipAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            {
                return bytes[0] == 10 ||
                       (bytes[0] == 172 && bytes[1] >= 16 && bytes[1] <= 31) ||
                       (bytes[0] == 192 && bytes[1] == 168) ||
                       (bytes[0] == 169 && bytes[1] == 254) ||
                       bytes[0] == 127;
            }

            // IPv6 checks for link-local and loopback
            if (ipAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
            {
                return ipAddress.IsIPv6LinkLocal || IPAddress.IsLoopback(ipAddress);
            }

            return false;
        }

        private void LogSuspiciousUrl(string url, string reason)
        {
            _logger.LogWarning("Suspicious URL blocked: {Url} - Reason: {Reason}", url, reason);
            // Note: Security audit logging should be handled at the controller/service level
            // where we have access to the HttpContext and can resolve scoped services
        }
    }
}
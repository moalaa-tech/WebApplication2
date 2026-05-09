using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Text.Json;

namespace CRM.WebApp.Security
{
    public interface ISecurityAuditService
    {
        Task LogSecurityEventAsync(SecurityEventType eventType, string description, object? additionalData = null);
        Task LogAccessAttemptAsync(string resource, bool success, string? reason = null);
        Task LogDataAccessAsync(string entityType, string entityId, DataAccessType accessType);
        Task LogConfigurationChangeAsync(string configurationKey, string oldValue, string newValue);
        Task LogSuspiciousActivityAsync(string activity, string details, SuspiciousActivityLevel level);
    }

    public class SecurityAuditService : ISecurityAuditService
    {
        private readonly ILogger<SecurityAuditService> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SecurityAuditService(ILogger<SecurityAuditService> logger, 
            IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task LogSecurityEventAsync(SecurityEventType eventType, string description, object? additionalData = null)
        {
            var securityEvent = new SecurityEvent
            {
                EventType = eventType,
                Description = description,
                Timestamp = DateTime.UtcNow,
                UserId = GetCurrentUserId(),
                UserName = GetCurrentUserName(),
                IpAddress = GetClientIpAddress(),
                UserAgent = GetUserAgent(),
                SessionId = GetSessionId(),
                AdditionalData = additionalData != null ? JsonSerializer.Serialize(additionalData) : null
            };

            // Log to structured logger
            _logger.LogInformation("Security Event: {EventType} - {Description} by User {UserId} from {IpAddress} at {Timestamp}",
                eventType, description, securityEvent.UserId, securityEvent.IpAddress, securityEvent.Timestamp);

            // Save to database if needed
            await SaveSecurityEventToDatabaseAsync(securityEvent);
        }

        public async Task LogAccessAttemptAsync(string resource, bool success, string? reason = null)
        {
            var eventType = success ? SecurityEventType.AccessGranted : SecurityEventType.AccessDenied;
            var description = $"Access {(success ? "granted" : "denied")} to {resource}";
            
            if (!success && !string.IsNullOrEmpty(reason))
            {
                description += $". Reason: {reason}";
            }

            await LogSecurityEventAsync(eventType, description, new { Resource = resource, Success = success, Reason = reason });
        }

        public async Task LogDataAccessAsync(string entityType, string entityId, DataAccessType accessType)
        {
            var description = $"{accessType} operation on {entityType} with ID {entityId}";
            await LogSecurityEventAsync(SecurityEventType.DataAccess, description, 
                new { EntityType = entityType, EntityId = entityId, AccessType = accessType });
        }

        public async Task LogConfigurationChangeAsync(string configurationKey, string oldValue, string newValue)
        {
            var description = $"Configuration changed: {configurationKey}";
            await LogSecurityEventAsync(SecurityEventType.ConfigurationChange, description,
                new { ConfigurationKey = configurationKey, OldValue = oldValue, NewValue = newValue });
        }

        public async Task LogSuspiciousActivityAsync(string activity, string details, SuspiciousActivityLevel level)
        {
            var eventType = level switch
            {
                SuspiciousActivityLevel.Low => SecurityEventType.SuspiciousActivityLow,
                SuspiciousActivityLevel.Medium => SecurityEventType.SuspiciousActivityMedium,
                SuspiciousActivityLevel.High => SecurityEventType.SuspiciousActivityHigh,
                _ => SecurityEventType.SuspiciousActivityMedium
            };

            await LogSecurityEventAsync(eventType, $"Suspicious activity: {activity}", 
                new { Activity = activity, Details = details, Level = level });

            // For high-level suspicious activities, also log as warning
            if (level == SuspiciousActivityLevel.High)
            {
                _logger.LogWarning("HIGH LEVEL SUSPICIOUS ACTIVITY: {Activity} - {Details} by User {UserId} from {IpAddress}",
                    activity, details, GetCurrentUserId(), GetClientIpAddress());
            }
        }

        private string GetCurrentUserId()
        {
            return _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Anonymous";
        }

        private string GetCurrentUserName()
        {
            return _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value ?? "Anonymous";
        }

        private string GetClientIpAddress()
        {
            var context = _httpContextAccessor.HttpContext;
            if (context == null) return "Unknown";

            // Check for forwarded IP first (in case of proxy/load balancer)
            var forwardedFor = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (!string.IsNullOrEmpty(forwardedFor))
            {
                return forwardedFor.Split(',')[0].Trim();
            }

            var realIp = context.Request.Headers["X-Real-IP"].FirstOrDefault();
            if (!string.IsNullOrEmpty(realIp))
            {
                return realIp;
            }

            return context.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
        }

        private string GetUserAgent()
        {
            return _httpContextAccessor.HttpContext?.Request.Headers["User-Agent"].FirstOrDefault() ?? "Unknown";
        }

        private string GetSessionId()
        {
            return _httpContextAccessor.HttpContext?.Session?.Id ?? 
                   _httpContextAccessor.HttpContext?.User?.FindFirst("session_id")?.Value ?? 
                   "Unknown";
        }

        private async Task SaveSecurityEventToDatabaseAsync(SecurityEvent securityEvent)
        {
            try
            {
                // In a real implementation, you would save this to your database
                // For now, we'll just log it
                var eventJson = JsonSerializer.Serialize(securityEvent, new JsonSerializerOptions { WriteIndented = false });
                _logger.LogInformation("Security Event Saved: {SecurityEventJson}", eventJson);
                
                await Task.CompletedTask; // Placeholder for database save
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to save security event to database");
            }
        }

    }

    public class SecurityEvent
    {
        public SecurityEventType EventType { get; set; }
        public required string Description { get; set; }
        public DateTime Timestamp { get; set; }
        public required string UserId { get; set; }
        public required string UserName { get; set; }
        public required string IpAddress { get; set; }
        public required string UserAgent { get; set; }
        public required string SessionId { get; set; }
        public string? AdditionalData { get; set; }
    }

    public enum SecurityEventType
    {
        LoginSuccess,
        LoginFailure,
        Logout,
        AccessGranted,
        AccessDenied,
        DataAccess,
        DataModification,
        ConfigurationChange,
        SuspiciousActivityLow,
        SuspiciousActivityMedium,
        SuspiciousActivityHigh,
        SecurityViolation,
        AccountLockout,
        PasswordChange,
        PermissionChange
    }

    public enum DataAccessType
    {
        Read,
        Create,
        Update,
        Delete
    }

    public enum SuspiciousActivityLevel
    {
        Low,
        Medium,
        High
    }
}
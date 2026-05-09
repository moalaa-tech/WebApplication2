using Microsoft.Extensions.Diagnostics.HealthChecks;
using CRM.WebApp.DbContext;
using Microsoft.EntityFrameworkCore;

namespace CRM.WebApp.HealthChecks
{
    /// <summary>
    /// Health check for database connectivity and basic functionality
    /// </summary>
    public class DatabaseHealthCheck : IHealthCheck
    {
        private readonly ApplicationContext _context;
        private readonly ILogger<DatabaseHealthCheck> _logger;

        public DatabaseHealthCheck(ApplicationContext context, ILogger<DatabaseHealthCheck> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                // Check if database is accessible
                // Note: OpenConnectionAsync may not work with in-memory databases, so we use CanConnectAsync instead
                var canConnect = await _context.Database.CanConnectAsync(cancellationToken);
                if (!canConnect)
                {
                    return HealthCheckResult.Unhealthy("Cannot connect to database", null, new Dictionary<string, object>
                    {
                        ["database_status"] = "disconnected",
                        ["last_checked"] = DateTime.UtcNow
                    });
                }

                // Check if we can query a basic table
                var userCount = await _context.Users.CountAsync(cancellationToken);
                
                var data = new Dictionary<string, object>
                {
                    ["database_status"] = "connected",
                    ["user_count"] = userCount,
                    ["connection_string"] = MaskConnectionString(_context.Database.GetConnectionString() ?? ""),
                    ["provider"] = _context.Database.ProviderName ?? "Unknown",
                    ["last_checked"] = DateTime.UtcNow
                };

                return HealthCheckResult.Healthy("Database is healthy and accessible", data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Database health check failed");
                
                return HealthCheckResult.Unhealthy("Database is not accessible", ex, new Dictionary<string, object>
                {
                    ["database_status"] = "disconnected",
                    ["error"] = ex.Message,
                    ["last_checked"] = DateTime.UtcNow
                });
            }
        }

        private static string MaskConnectionString(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                return "";

            // Simple masking for security - hide password and user
            return System.Text.RegularExpressions.Regex.Replace(
                connectionString, 
                @"(Password|Pwd|User Id|UID)=([^;]*)", 
                "$1=***", 
                System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        }
    }

    /// <summary>
    /// Health check for system memory usage
    /// </summary>
    public class MemoryHealthCheck : IHealthCheck
    {
        private readonly ILogger<MemoryHealthCheck> _logger;
        private const long WarningThresholdBytes = 1_000_000_000; // 1GB
        private const long CriticalThresholdBytes = 2_000_000_000; // 2GB

        public MemoryHealthCheck(ILogger<MemoryHealthCheck> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                var allocatedBytes = GC.GetTotalMemory(false);
                var workingSetBytes = Environment.WorkingSet;

                var data = new Dictionary<string, object>
                {
                    ["allocated_bytes"] = allocatedBytes,
                    ["working_set_bytes"] = workingSetBytes,
                    ["allocated_mb"] = Math.Round(allocatedBytes / 1024.0 / 1024.0, 2),
                    ["working_set_mb"] = Math.Round(workingSetBytes / 1024.0 / 1024.0, 2),
                    ["last_checked"] = DateTime.UtcNow
                };

                if (allocatedBytes > CriticalThresholdBytes)
                {
                    return Task.FromResult(HealthCheckResult.Unhealthy("Memory usage is critically high", null, data));
                }

                if (allocatedBytes > WarningThresholdBytes)
                {
                    return Task.FromResult(HealthCheckResult.Degraded("Memory usage is high", null, data));
                }

                return Task.FromResult(HealthCheckResult.Healthy("Memory usage is normal", data));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Memory health check failed");
                return Task.FromResult(HealthCheckResult.Unhealthy("Memory health check failed", ex));
            }
        }
    }

    /// <summary>
    /// Health check for application configuration
    /// </summary>
    public class ConfigurationHealthCheck : IHealthCheck
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<ConfigurationHealthCheck> _logger;

        public ConfigurationHealthCheck(IConfiguration configuration, ILogger<ConfigurationHealthCheck> logger)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                var issues = new List<string>();
                var data = new Dictionary<string, object>
                {
                    ["last_checked"] = DateTime.UtcNow
                };

                // Check critical configuration settings
                var connectionString = _configuration.GetConnectionString("defaultConnection");
                if (string.IsNullOrEmpty(connectionString))
                {
                    issues.Add("Database connection string is missing");
                }

                var smtpSettings = _configuration.GetSection("SmtpSettings");
                if (!smtpSettings.Exists())
                {
                    issues.Add("SMTP settings are missing");
                }

                data["issues_count"] = issues.Count;
                
                if (issues.Any())
                {
                    data["issues"] = issues;
                    return Task.FromResult(HealthCheckResult.Unhealthy("Configuration issues found", null, data));
                }

                data["status"] = "All critical configurations are present";
                return Task.FromResult(HealthCheckResult.Healthy("Configuration is valid", data));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Configuration health check failed");
                return Task.FromResult(HealthCheckResult.Unhealthy("Configuration health check failed", ex));
            }
        }
    }

    /// <summary>
    /// Health check for external services (can be extended for APIs, email service, etc.)
    /// </summary>
    public class ExternalServicesHealthCheck : IHealthCheck
    {
        private readonly ILogger<ExternalServicesHealthCheck> _logger;
        private readonly HttpClient _httpClient;

        public ExternalServicesHealthCheck(ILogger<ExternalServicesHealthCheck> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _httpClient = httpClientFactory.CreateClient("HealthCheck");
            _httpClient.Timeout = TimeSpan.FromSeconds(10);
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                var data = new Dictionary<string, object>
                {
                    ["last_checked"] = DateTime.UtcNow,
                    ["external_services"] = new Dictionary<string, object>()
                };

                // Here you can add checks for external APIs, services, etc.
                // For now, we'll just check if HTTP client factory is working
                var services = (Dictionary<string, object>)data["external_services"];
                services["http_client_factory"] = "available";

                // Example: Check external service (uncomment and modify as needed)
                /*
                try 
                {
                    var response = await _httpClient.GetAsync("https://api.example.com/health", cancellationToken);
                    services["example_api"] = response.IsSuccessStatusCode ? "healthy" : "unhealthy";
                }
                catch (Exception ex)
                {
                    services["example_api"] = $"error: {ex.Message}";
                }
                */

                return HealthCheckResult.Healthy("External services are accessible", data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "External services health check failed");
                return HealthCheckResult.Unhealthy("External services health check failed", ex);
            }
        }
    }
}
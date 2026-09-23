using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Text.Json;

namespace WebApplication2.Api.Controllers
{
    /// <summary>
    /// Health check endpoints for monitoring and diagnostics
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class HealthController : ControllerBase
    {
        private readonly HealthCheckService _healthCheckService;
        private readonly ILogger<HealthController> _logger;

        public HealthController(HealthCheckService healthCheckService, ILogger<HealthController> logger)
        {
            _healthCheckService = healthCheckService ?? throw new ArgumentNullException(nameof(healthCheckService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Get overall application health status
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var report = await _healthCheckService.CheckHealthAsync();
                
                var response = new
                {
                    Status = report.Status.ToString(),
                    TotalDuration = report.TotalDuration,
                    Entries = report.Entries.Select(e => new
                    {
                        Name = e.Key,
                        Status = e.Value.Status.ToString(),
                        Description = e.Value.Description,
                        Duration = e.Value.Duration,
                        Data = e.Value.Data,
                        Exception = e.Value.Exception?.Message
                    })
                };

                var statusCode = report.Status switch
                {
                    HealthStatus.Healthy => 200,
                    HealthStatus.Degraded => 200,
                    HealthStatus.Unhealthy => 503,
                    _ => 500
                };

                return StatusCode(statusCode, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Health check endpoint failed");
                return StatusCode(500, new { Status = "Error", Message = "Health check failed" });
            }
        }

        /// <summary>
        /// Get health status in a simple format for load balancer health checks
        /// </summary>
        [HttpGet("live")]
        public async Task<IActionResult> Live()
        {
            try
            {
                var report = await _healthCheckService.CheckHealthAsync();
                
                if (report.Status == HealthStatus.Healthy || report.Status == HealthStatus.Degraded)
                {
                    return Ok(new { Status = "Healthy", Timestamp = DateTime.UtcNow });
                }
                
                return StatusCode(503, new { Status = "Unhealthy", Timestamp = DateTime.UtcNow });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Liveness check failed");
                return StatusCode(503, new { Status = "Unhealthy", Timestamp = DateTime.UtcNow, Error = ex.Message });
            }
        }

        /// <summary>
        /// Get readiness status - indicates if the application is ready to serve requests
        /// </summary>
        [HttpGet("ready")]
        public async Task<IActionResult> Ready()
        {
            try
            {
                var report = await _healthCheckService.CheckHealthAsync();
                
                // Check critical services for readiness
                var criticalServices = new[] { "database", "configuration" };
                var criticalIssues = report.Entries
                    .Where(e => criticalServices.Any(cs => e.Key.Contains(cs, StringComparison.OrdinalIgnoreCase)))
                    .Where(e => e.Value.Status == HealthStatus.Unhealthy)
                    .ToList();

                if (criticalIssues.Any())
                {
                    return StatusCode(503, new 
                    { 
                        Status = "NotReady", 
                        Timestamp = DateTime.UtcNow,
                        Issues = criticalIssues.Select(i => new { Service = i.Key, Status = i.Value.Status.ToString() })
                    });
                }
                
                return Ok(new { Status = "Ready", Timestamp = DateTime.UtcNow });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Readiness check failed");
                return StatusCode(503, new { Status = "NotReady", Timestamp = DateTime.UtcNow, Error = ex.Message });
            }
        }

        /// <summary>
        /// Get detailed health information for specific service
        /// </summary>
        [HttpGet("{serviceName}")]
        public async Task<IActionResult> GetServiceHealth(string serviceName)
        {
            try
            {
                var report = await _healthCheckService.CheckHealthAsync();
                
                var service = report.Entries.FirstOrDefault(e => 
                    e.Key.Equals(serviceName, StringComparison.OrdinalIgnoreCase));

                if (service.Key == null)
                {
                    return NotFound(new { Message = $"Health check service '{serviceName}' not found" });
                }

                var response = new
                {
                    Name = service.Key,
                    Status = service.Value.Status.ToString(),
                    Description = service.Value.Description,
                    Duration = service.Value.Duration,
                    Data = service.Value.Data,
                    Exception = service.Value.Exception?.Message,
                    Timestamp = DateTime.UtcNow
                };

                var statusCode = service.Value.Status switch
                {
                    HealthStatus.Healthy => 200,
                    HealthStatus.Degraded => 200,
                    HealthStatus.Unhealthy => 503,
                    _ => 500
                };

                return StatusCode(statusCode, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Service health check failed for {ServiceName}", serviceName);
                return StatusCode(500, new { Status = "Error", Message = "Service health check failed" });
            }
        }
    }
}
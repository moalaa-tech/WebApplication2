using CRM.WebApp.DTOs.LoggingDto;
using CRM.WebApp.Services.Interfaces;
using System.Diagnostics;

namespace CRM.WebApp.Middlewares
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;
        private readonly ILogService _logService;

        public RequestLoggingMiddleware(
            RequestDelegate next,
            ILogger<RequestLoggingMiddleware> logger,
            ILogService logService)
        {
            _next = next;
            _logger = logger;
            _logService = logService;
        }

        public async Task Invoke(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();
            var originalRequestBody = context.Request.Body;
            var originalResponseBody = context.Response.Body;

            try
            {
                // Read request body
                string requestBody = null;
                if (context.Request.ContentLength.HasValue && context.Request.ContentLength > 0)
                {
                    context.Request.EnableBuffering();
                    using (var reader = new StreamReader(context.Request.Body, leaveOpen: true))
                    {
                        requestBody = await reader.ReadToEndAsync();
                        context.Request.Body.Position = 0;
                    }
                }

                // Capture response
                using (var responseBody = new MemoryStream())
                {
                    context.Response.Body = responseBody;

                    await _next(context);

                    // Read response body
                    string responseBodyContent = null;
                    if (context.Response.ContentLength.HasValue && context.Response.ContentLength > 0)
                    {
                        responseBody.Position = 0;
                        responseBodyContent = await new StreamReader(responseBody).ReadToEndAsync();
                        responseBody.Position = 0;
                        await responseBody.CopyToAsync(originalResponseBody);
                    }

                    stopwatch.Stop();

                    // Log to database
                    var logEntry = new CreateLogDto
                    {
                        Timestamp = DateTime.UtcNow,
                        Level = LogLevel.Information.ToString(),
                        Message = $"HTTP {context.Request.Method} {context.Request.Path} responded {context.Response.StatusCode}",
                        Url = $"{context.Request.Path}{context.Request.QueryString}",
                        HttpMethod = context.Request.Method,
                        UserName = context.User?.Identity?.Name,
                        ClientIP = context.Connection.RemoteIpAddress?.ToString(),
                        StatusCode = context.Response.StatusCode,
                        RequestBody = requestBody,
                        ResponseBody = responseBodyContent,
                        Duration = stopwatch.ElapsedMilliseconds
                    };

                    try
                    {
                        await _logService.CreateLogAsync(logEntry);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Failed to log request to database");
                    }
                }
            }
            catch (Exception ex)
            {
                stopwatch.Stop();

                // Log error to database
                var logEntry = new CreateLogDto
                {
                    Timestamp = DateTime.UtcNow,
                    Level = LogLevel.Error.ToString(),
                    Message = $"HTTP {context.Request.Method} {context.Request.Path} failed: {ex.Message}",
                    Exception = ex.ToString(),
                    Url = $"{context.Request.Path}{context.Request.QueryString}",
                    HttpMethod = context.Request.Method,
                    UserName = context.User?.Identity?.Name,
                    ClientIP = context.Connection.RemoteIpAddress?.ToString(),
                    Duration = stopwatch.ElapsedMilliseconds
                };

                try
                {
                    await _logService.CreateLogAsync(logEntry);
                }
                catch (Exception dbEx)
                {
                    _logger.LogError(dbEx, "Failed to log error to database");
                }

                // Restore original response body
                context.Response.Body = originalResponseBody;
                throw;
            }
            finally
            {
                context.Request.Body = originalRequestBody;
                context.Response.Body = originalResponseBody;
            }
        }
    }
}

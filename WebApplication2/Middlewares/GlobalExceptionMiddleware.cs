using System.Security;
using System.Text.Json;
using CRM.WebApp.Security;
using CRM.WebApp.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CRM.WebApp.Middlewares
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;
        private readonly IWebHostEnvironment _environment;

        public GlobalExceptionMiddleware(RequestDelegate next, 
            ILogger<GlobalExceptionMiddleware> logger,
            IWebHostEnvironment environment)
        {
            _next = next;
            _logger = logger;
            _environment = environment;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var correlationId = context.TraceIdentifier ?? Guid.NewGuid().ToString();
            
            // Log security-related exceptions
            if (IsSecurityException(ex))
            {
                try
                {
                    var securityAuditService = context.RequestServices.GetService<ISecurityAuditService>();
                    if (securityAuditService != null)
                    {
                        await securityAuditService.LogSuspiciousActivityAsync(
                            "Security Exception", 
                            $"Exception: {ex.GetType().Name} - Path: {context.Request.Path} - User: {context.User?.Identity?.Name ?? "Anonymous"}", 
                            SuspiciousActivityLevel.High);
                    }
                }
                catch (Exception auditEx)
                {
                    _logger.LogError(auditEx, "Failed to log security exception to audit service");
                }
            }

            // Get user context for logging
            var userIdentifier = context.User?.Identity?.Name ?? "Anonymous";
            var userAgent = context.Request.Headers["User-Agent"].ToString();
            var ipAddress = context.Connection.RemoteIpAddress?.ToString();

            // Log detailed error for internal use
            _logger.LogError(ex, 
                "Unhandled exception occurred. CorrelationId: {CorrelationId}, Path: {Path}, Method: {Method}, User: {User}, UserAgent: {UserAgent}, IP: {IPAddress}",
                correlationId, context.Request.Path, context.Request.Method, userIdentifier, userAgent, ipAddress);

            var statusCode = GetStatusCode(ex);
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";

            var response = CreateErrorResponse(ex, correlationId, statusCode);
            
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = _environment.IsDevelopment()
            };
            
            await context.Response.WriteAsync(JsonSerializer.Serialize(response, options));
        }

        private bool IsSecurityException(Exception ex)
        {
            return ex is UnauthorizedAccessException ||
                   ex is SecurityException ||
                   ex.GetType().Name.Contains("Security") ||
                   ex.Message.Contains("unauthorized", StringComparison.OrdinalIgnoreCase) ||
                   ex.Message.Contains("forbidden", StringComparison.OrdinalIgnoreCase);
        }

        private int GetStatusCode(Exception ex)
        {
            return ex switch
            {
                BusinessException businessEx => businessEx.StatusCode,
                UnauthorizedAccessException => 401,
                ArgumentNullException => 400,
                ArgumentException => 400,
                KeyNotFoundException => 404,
                NotImplementedException => 501,
                TimeoutException => 408,
                InvalidOperationException => 409,
                _ => 500
            };
        }

        private ErrorResponse CreateErrorResponse(Exception ex, string correlationId, int statusCode)
        {
            var message = GetUserFriendlyMessage(ex);
            
            var errorResponse = new ErrorResponse(message, correlationId, statusCode);

            if (_environment.IsDevelopment())
            {
                errorResponse.Details = ex.Message;
                errorResponse.Type = ex.GetType().Name;
            }

            // Add validation errors if available
            if (ex is BusinessException businessEx && businessEx.ValidationErrors != null)
            {
                errorResponse.ValidationErrors = businessEx.ValidationErrors;
            }

            return errorResponse;
        }

        private string GetUserFriendlyMessage(Exception ex)
        {
            return ex switch
            {
                BusinessException businessEx => businessEx.Message,
                UnauthorizedAccessException => "You are not authorized to access this resource.",
                ArgumentNullException => "Required data is missing.",
                ArgumentException => "Invalid request data provided.",
                TimeoutException => "The request timed out. Please try again.",
                InvalidOperationException => "The requested operation is not valid in the current state.",
                _ => "An unexpected error occurred. Please try again or contact support."
            };
        }


    }
}

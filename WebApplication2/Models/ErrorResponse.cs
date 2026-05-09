using System.Text.Json.Serialization;

namespace CRM.WebApp.Models
{
    /// <summary>
    /// Standard error response model for API consistency
    /// </summary>
    public class ErrorResponse
    {
        public string Message { get; set; }
        public string CorrelationId { get; set; }
        public int StatusCode { get; set; }
        public DateTime Timestamp { get; set; }
        public string? Details { get; set; }
        public string? Type { get; set; }
        public Dictionary<string, string[]>? ValidationErrors { get; set; }

        public ErrorResponse(string message, string correlationId, int statusCode)
        {
            Message = message ?? "An error occurred";
            CorrelationId = correlationId ?? Guid.NewGuid().ToString();
            StatusCode = statusCode;
            Timestamp = DateTime.UtcNow;
        }

        [JsonConstructor]
        public ErrorResponse(string message, string correlationId, int statusCode, DateTime timestamp, 
            string? details = null, string? type = null, Dictionary<string, string[]>? validationErrors = null)
            : this(message, correlationId, statusCode)
        {
            Timestamp = timestamp;
            Details = details;
            Type = type;
            ValidationErrors = validationErrors;
        }
    }

    /// <summary>
    /// Business exception for controlled error handling
    /// </summary>
    public class BusinessException : Exception
    {
        public int StatusCode { get; }
        public Dictionary<string, string[]>? ValidationErrors { get; }

        public BusinessException(string message, int statusCode = 400) : base(message)
        {
            StatusCode = statusCode;
        }

        public BusinessException(string message, Dictionary<string, string[]> validationErrors, int statusCode = 400) 
            : base(message)
        {
            StatusCode = statusCode;
            ValidationErrors = validationErrors;
        }

        public BusinessException(string message, Exception innerException, int statusCode = 400) 
            : base(message, innerException)
        {
            StatusCode = statusCode;
        }
    }

    /// <summary>
    /// Not found exception for 404 responses
    /// </summary>
    public class NotFoundException : BusinessException
    {
        public NotFoundException(string message) : base(message, 404)
        {
        }

        public NotFoundException(string entityName, int id) 
            : base($"{entityName} with ID {id} was not found.", 404)
        {
        }

        public NotFoundException(string entityName, string identifier) 
            : base($"{entityName} with identifier '{identifier}' was not found.", 404)
        {
        }
    }

    /// <summary>
    /// Validation exception for 400 responses with detailed validation errors
    /// </summary>
    public class ValidationException : BusinessException
    {
        public ValidationException(string message) : base(message, 400)
        {
        }

        public ValidationException(Dictionary<string, string[]> validationErrors) 
            : base("Validation failed.", validationErrors, 400)
        {
        }

        public ValidationException(string field, string error) 
            : base("Validation failed.", new Dictionary<string, string[]> { { field, new[] { error } } }, 400)
        {
        }
    }

    /// <summary>
    /// Unauthorized operation exception for 403 responses
    /// </summary>
    public class UnauthorizedOperationException : BusinessException
    {
        public UnauthorizedOperationException(string message) : base(message, 403)
        {
        }

        public UnauthorizedOperationException() : base("You are not authorized to perform this operation.", 403)
        {
        }
    }
}
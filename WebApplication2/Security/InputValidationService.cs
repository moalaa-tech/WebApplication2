using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Web;
using Microsoft.AspNetCore.Html;

namespace CRM.WebApp.Security
{
    public interface IInputValidationService
    {
        bool IsValidEmail(string email);
        bool IsValidPhoneNumber(string phoneNumber);
        bool IsSafeInput(string input);
        string SanitizeInput(string input);
        string SanitizeHtml(string html);
        bool ContainsSqlInjectionPatterns(string input);
        bool ContainsXssPatterns(string input);
        string ValidateAndSanitizeFileName(string fileName);
    }

    public class InputValidationService : IInputValidationService
    {
        private readonly ILogger<InputValidationService> _logger;
        
        // SQL Injection patterns
        private readonly string[] _sqlInjectionPatterns = {
            @"(\b(ALTER|CREATE|DELETE|DROP|EXEC(UTE)?|INSERT|MERGE|SELECT|UPDATE|UNION|USE)\b)",
            @"(\b(INFORMATION_SCHEMA|SYSOBJECTS|SYSCOLUMNS)\b)",
            @"('(''|[^'])*')",
            @"(;|\s)(EXEC|EXECUTE)\s",
            @"(;|\s)(SP_|XP_)\w+",
            @"(\b(AND|OR)\b\s+\w+\s*=\s*\w+)",
            @"(\b(HAVING|ORDER\s+BY|GROUP\s+BY)\b)",
            @"(\b(WAITFOR\s+DELAY|BENCHMARK)\b)"
        };

        // XSS patterns
        private readonly string[] _xssPatterns = {
            @"<\s*script[^>]*>.*?</\s*script\s*>",
            @"<\s*iframe[^>]*>.*?</\s*iframe\s*>",
            @"<\s*object[^>]*>.*?</\s*object\s*>",
            @"<\s*embed[^>]*>.*?</\s*embed\s*>",
            @"<\s*applet[^>]*>.*?</\s*applet\s*>",
            @"javascript:",
            @"vbscript:",
            @"onload\s*=",
            @"onerror\s*=",
            @"onclick\s*=",
            @"onmouseover\s*=",
            @"eval\s*\(",
            @"expression\s*\("
        };

        public InputValidationService(ILogger<InputValidationService> logger)
        {
            _logger = logger;
        }

        public bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                var emailRegex = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", RegexOptions.IgnoreCase);
                return emailRegex.IsMatch(email) && email.Length <= 254;
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        public bool IsValidPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                return false;

            var phoneRegex = new Regex(@"^\+?[1-9]\d{1,14}$");
            return phoneRegex.IsMatch(phoneNumber.Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", ""));
        }

        public bool IsSafeInput(string input)
        {
            if (string.IsNullOrEmpty(input))
                return true;

            return !ContainsSqlInjectionPatterns(input) && !ContainsXssPatterns(input);
        }

        public string SanitizeInput(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            // Remove dangerous characters and patterns
            input = input.Replace("<", "&lt;")
                        .Replace(">", "&gt;")
                        .Replace("\"", "&quot;")
                        .Replace("'", "&#x27;")
                        .Replace("/", "&#x2F;");

            // Remove SQL injection patterns
            foreach (var pattern in _sqlInjectionPatterns)
            {
                input = Regex.Replace(input, pattern, "", RegexOptions.IgnoreCase);
            }

            return input;
        }

        public string SanitizeHtml(string html)
        {
            if (string.IsNullOrEmpty(html))
                return html;

            // Basic HTML sanitization - in production, consider using a library like HtmlSanitizer
            return HttpUtility.HtmlEncode(html);
        }

        public bool ContainsSqlInjectionPatterns(string input)
        {
            if (string.IsNullOrEmpty(input))
                return false;

            foreach (var pattern in _sqlInjectionPatterns)
            {
                try
                {
                    if (Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase))
                    {
                        _logger.LogWarning("SQL injection pattern detected: {Pattern} in input: {Input}", pattern, input);
                        return true;
                    }
                }
                catch (RegexMatchTimeoutException)
                {
                    _logger.LogWarning("Regex timeout while checking SQL injection pattern: {Pattern}", pattern);
                    return true; // Assume malicious on timeout
                }
            }

            return false;
        }

        public bool ContainsXssPatterns(string input)
        {
            if (string.IsNullOrEmpty(input))
                return false;

            foreach (var pattern in _xssPatterns)
            {
                try
                {
                    if (Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase))
                    {
                        _logger.LogWarning("XSS pattern detected: {Pattern} in input: {Input}", pattern, input);
                        return true;
                    }
                }
                catch (RegexMatchTimeoutException)
                {
                    _logger.LogWarning("Regex timeout while checking XSS pattern: {Pattern}", pattern);
                    return true; // Assume malicious on timeout
                }
            }

            return false;
        }

        public string ValidateAndSanitizeFileName(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                return null;

            // Remove path traversal attempts
            fileName = Path.GetFileName(fileName);
            
            // Remove invalid characters
            var invalidChars = Path.GetInvalidFileNameChars();
            foreach (var c in invalidChars)
            {
                fileName = fileName.Replace(c.ToString(), "");
            }

            // Limit length
            if (fileName.Length > 255)
                fileName = fileName.Substring(0, 255);

            return fileName;
        }
    }

    // Custom validation attributes
    public class NoSqlInjectionAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null) return true;
            
            var stringValue = value.ToString();
            var validator = new InputValidationService(null);
            return !validator.ContainsSqlInjectionPatterns(stringValue);
        }

        public override string FormatErrorMessage(string name)
        {
            return $"The {name} field contains potentially dangerous content.";
        }
    }

    public class NoXssAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null) return true;
            
            var stringValue = value.ToString();
            var validator = new InputValidationService(null);
            return !validator.ContainsXssPatterns(stringValue);
        }

        public override string FormatErrorMessage(string name)
        {
            return $"The {name} field contains potentially dangerous content.";
        }
    }
}
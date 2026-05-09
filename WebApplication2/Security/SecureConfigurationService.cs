using Microsoft.AspNetCore.DataProtection;
using System.Security.Cryptography;
using System.Text;

namespace CRM.WebApp.Security
{
    public interface ISecureConfigurationService
    {
        string EncryptConnectionString(string connectionString);
        string DecryptConnectionString(string encryptedConnectionString);
        string HashSensitiveData(string data);
        bool VerifyHashedData(string data, string hashedData);
        string GenerateSecureToken();
    }

    public class SecureConfigurationService : ISecureConfigurationService
    {
        private readonly IDataProtector _protector;
        private readonly ILogger<SecureConfigurationService> _logger;

        public SecureConfigurationService(IDataProtectionProvider dataProtectionProvider, ILogger<SecureConfigurationService> logger)
        {
            _protector = dataProtectionProvider.CreateProtector("CRM.SecureConfiguration.v1");
            _logger = logger;
        }

        public string EncryptConnectionString(string connectionString)
        {
            try
            {
                return _protector.Protect(connectionString);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to encrypt connection string");
                throw;
            }
        }

        public string DecryptConnectionString(string encryptedConnectionString)
        {
            try
            {
                return _protector.Unprotect(encryptedConnectionString);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to decrypt connection string");
                throw;
            }
        }

        public string HashSensitiveData(string data)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(data + GetSalt()));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        public bool VerifyHashedData(string data, string hashedData)
        {
            var computedHash = HashSensitiveData(data);
            return computedHash == hashedData;
        }

        public string GenerateSecureToken()
        {
            using (var rng = RandomNumberGenerator.Create())
            {
                var bytes = new byte[32];
                rng.GetBytes(bytes);
                return Convert.ToBase64String(bytes);
            }
        }

        private string GetSalt()
        {
            // In production, store this securely or use a key vault
            var salt = Environment.GetEnvironmentVariable("CRM_SALT");
            if (string.IsNullOrEmpty(salt))
            {
                _logger.LogWarning("CRM_SALT environment variable not set, using default salt. This should be configured in production.");
                return "DefaultSalt123!@#"; // Only for development
            }
            return salt;
        }
    }

    public static class SecurityHeaders
    {
        public static void AddSecurityHeaders(this IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                // Security headers - check if already exists to prevent duplicates
                if (!context.Response.Headers.ContainsKey("X-Content-Type-Options"))
                    context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
                
                if (!context.Response.Headers.ContainsKey("X-Frame-Options"))
                    context.Response.Headers.Add("X-Frame-Options", "DENY");
                
                if (!context.Response.Headers.ContainsKey("X-XSS-Protection"))
                    context.Response.Headers.Add("X-XSS-Protection", "1; mode=block");
                
                if (!context.Response.Headers.ContainsKey("Referrer-Policy"))
                    context.Response.Headers.Add("Referrer-Policy", "strict-origin-when-cross-origin");
                
                if (!context.Response.Headers.ContainsKey("Content-Security-Policy"))
                    context.Response.Headers.Add("Content-Security-Policy", 
                        "default-src 'self'; " +
                        "connect-src 'self' ws: wss:; " +
                        "script-src 'self' 'unsafe-inline' 'unsafe-eval' https://cdnjs.cloudflare.com https://cdn.jsdelivr.net; " +
                        "style-src 'self' 'unsafe-inline' https://cdnjs.cloudflare.com https://fonts.googleapis.com; " +
                        "font-src 'self' https://fonts.gstatic.com; " +
                        "img-src 'self' data: https:; " +
                        "connect-src 'self';");
                
                // Remove server information
                context.Response.Headers.Remove("Server");
                
                await next();
            });
        }
    }
}
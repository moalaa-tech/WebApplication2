using CRM.Domain.Entities;
using CRM.WebApp.DbContext;
using CRM.WebApp.DTOs.LoggingDto;
using CRM.WebApp.Services.Interfaces;
using System.Diagnostics;

namespace CRM.WebApp.Logging
{
    public class DatabaseLogger : ILogger
    {
        private readonly string _categoryName;
        private readonly ApplicationContext _dbContext;

        public DatabaseLogger(string categoryName, ApplicationContext dbContext)
        {
            _categoryName = categoryName;
            _dbContext = dbContext;
        }

        public IDisposable? BeginScope<TState>(TState state) => null;

        public bool IsEnabled(LogLevel logLevel) => true;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            //if (!IsEnabled(logLevel)) return;

            var message = formatter(state, exception);
            if (string.IsNullOrEmpty(message) && exception == null) return;

            LogEntry logEntry = new LogEntry
            {
                Level = logLevel.ToString(),
                Message = message,
                Exception = exception?.ToString() ?? "Exception",
                Logger = _categoryName,
                Timestamp = DateTime.UtcNow,
                RequestBody = string.Empty,
                UserName = "",
                ResponseBody = "",
                HttpMethod = "",
                Duration = 0,
                Url = string.Empty,
                ClientIP = string.Empty,
                StatusCode = 0
            };

            try
            {
                _dbContext.Logs.Add(logEntry);
                _dbContext.SaveChanges();                
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to log to database: {ex.Message}");
            }
        }


    }
}

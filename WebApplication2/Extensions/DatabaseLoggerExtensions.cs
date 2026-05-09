
using CRM.WebApp.Providers;
using CRM.WebApp.Services.Interfaces;

namespace CRM.WebApp.Extensions
{
    public static class DatabaseLoggerExtensions
    {
        public static ILoggingBuilder AddDatabaseLogger(this ILoggingBuilder builder, Func<string, LogLevel, bool> filter = null)
        {
           // builder.Services.AddSingleton<ILoggerProvider>(serviceProvider => new DatabaseLoggerProvider(filter, serviceProvider.GetRequiredService<ILogService>()));
            return builder;
        }
    }
}

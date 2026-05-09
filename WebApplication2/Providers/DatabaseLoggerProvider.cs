using CRM.WebApp.DbContext;
using CRM.WebApp.Logging;
using CRM.WebApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CRM.WebApp.Providers
{
    public class DatabaseLoggerProvider : ILoggerProvider
    {
        
        private readonly ApplicationContext _dbContextFactory;

 

        public DatabaseLoggerProvider(ApplicationContext dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }



        public ILogger CreateLogger(string categoryName)
        {
            return new DatabaseLogger(categoryName, _dbContextFactory);
        }

        public void Dispose()
        {
        }
    }
}

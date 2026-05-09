using CRM.WebApi.DbContext;
using System.Diagnostics;

namespace CRM.WebApi.Middlewares
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private ApplicationContext dbContext;


        public RequestLoggingMiddleware(RequestDelegate next, ApplicationContext _dbContext)
        {
            _next = next;
            dbContext = _dbContext;

        }

        public async Task Invoke(HttpContext context, ApplicationContext dbContext)
        {
            var stopwatch = Stopwatch.StartNew();
            var originalRequestBody = context.Request.Body;
            var originalResponseBody = context.Response.Body;


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

            await _next(context);

            // Capture response
            using (var responseBody = new MemoryStream())
            {
                context.Response.Body = responseBody;

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
                var logEntry = new LogEntry
                {
                    Timestamp = DateTime.UtcNow,
                    Level = LogLevel.Information.ToString(),
                    Message = $"HTTP {context.Request.Method} {context.Request.Path} responded {context.Response.StatusCode}",
                    Url = $"{context.Request.Path}{context.Request.QueryString}",
                    HttpMethod = context.Request.Method,
                    UserName = context.User?.Identity?.Name,
                    ClientIP = context.Connection.RemoteIpAddress?.ToString(),
                    StatusCode = context.Response.StatusCode,
                    RequestBody =  requestBody,
                    ResponseBody = responseBodyContent,
                    Duration = stopwatch.ElapsedMilliseconds,
                    Logger = "RequestLoggingMiddleware",
                    Exception = null
                };

                dbContext.Logs.Add(logEntry);
                await dbContext.SaveChangesAsync();

                await responseBody.CopyToAsync(originalResponseBody);
            }


        }
    }
}

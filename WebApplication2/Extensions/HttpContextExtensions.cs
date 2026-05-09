namespace CRM.WebApp.Extensions
{
    public static class HttpContextExtensions
    {
        public static string? GetClientIp(this HttpContext context)
        {
            return context.Connection?.RemoteIpAddress?.ToString();
        }

        public static async Task<string> ReadBodyAsStringAsync(this HttpRequest request)
        {
            request.EnableBuffering(); // Allows rereading
            request.Body.Position = 0;
            using var reader = new StreamReader(request.Body, leaveOpen: true);
            var body = await reader.ReadToEndAsync();
            request.Body.Position = 0;
            return body;
        }

    }
}

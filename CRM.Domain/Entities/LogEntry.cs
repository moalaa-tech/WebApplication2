namespace CRM.Domain.Entities
{
    public class LogEntry
    {
        public int Id { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.UtcNow;


        public string Level { get; set; } // Information, Warning, Error, Critical

        public string Message { get; set; }

        public string Exception { get; set; }

        public string Logger { get; set; } // The logger name (usually the class name)

        public string Url { get; set; }

        public string HttpMethod { get; set; }

        public string UserName { get; set; }

        public string ClientIP { get; set; }

        public int? StatusCode { get; set; }

        public string RequestBody { get; set; }

        public string ResponseBody { get; set; }

        public long? Duration { get; set; } // Request duration in milliseconds
    }
}

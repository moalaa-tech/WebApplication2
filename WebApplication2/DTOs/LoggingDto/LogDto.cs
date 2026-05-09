namespace CRM.WebApp.DTOs.LoggingDto
{
    public class LogDto
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string Level { get; set; }
        public string Message { get; set; }
        public string Exception { get; set; }
        public string Logger { get; set; }
        public string Url { get; set; }
        public string HttpMethod { get; set; }
        public string UserName { get; set; }
        public string ClientIP { get; set; }
        public int? StatusCode { get; set; }
        public string RequestBody { get; set; }
        public string ResponseBody { get; set; }
        public long? Duration { get; set; }
    }
}

namespace CRM.WebApp.DTOs.LoggingDto
{
    public class LogFilterDto
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 50;
        public string Level { get; set; }
        public string Search { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}

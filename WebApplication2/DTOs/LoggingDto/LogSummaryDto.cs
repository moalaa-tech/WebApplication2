namespace CRM.WebApp.DTOs.LoggingDto
{
    public class LogSummaryDto
    {
        public int TotalCount { get; set; }
        public int InformationCount { get; set; }
        public int WarningCount { get; set; }
        public int ErrorCount { get; set; }
        public int CriticalCount { get; set; }
    }
}

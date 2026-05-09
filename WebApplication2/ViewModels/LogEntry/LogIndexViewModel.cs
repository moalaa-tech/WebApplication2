using CRM.WebApp.DTOs.LoggingDto;

namespace CRM.WebApp.ViewModels.LogEntry
{
    public class LogIndexViewModel
    {
        public IEnumerable<LogDto> Logs { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public string LevelFilter { get; set; }
        public string SearchFilter { get; set; }
        public LogSummaryDto LogSummary { get; set; }

        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;

    }
}

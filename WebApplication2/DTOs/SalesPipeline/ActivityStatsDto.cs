namespace CRM.WebApp.DTOs.SalesPipeline
{
    public class ActivityStatsDto
    {
        public int TotalActivities { get; set; }
        public int CompletedActivities { get; set; }
        public int OverdueActivities { get; set; }
        public Dictionary<string, int> ActivitiesByType { get; set; }
        public decimal CompletionRate { get; set; }
    }
}

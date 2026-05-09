namespace CRM.WebApp.DTOs.FollowUpTask
{
    public class CreateFollowUpTaskDto
    {
        public string? Subject { get; set; }
        public DateTime DueDate { get; set; }
        public string? ContactName { get; set; }
        public int Status { get; set; }
        public int Priority { get; set; }
        public string? Description { get; set; }
        public int CreatedByID { get; set; }
        public bool IsRecurring { get; set; }
        public int? RecurrenceDays { get; set; } // e.g., 7 for weekly
    }
}

namespace CRM.WebApp.DTOs.Project
{
    public class TimeEntryDto
    {
        public int Id { get; set; }
        public int JobPhaseId { get; set; }
        public string PhaseName { get; set; }

        // User Information
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }

        // Entry Details
        public DateTime EntryDate { get; set; }
        public decimal Hours { get; set; }
        public string Description { get; set; }

        // Financial Information
        public bool IsBillable { get; set; }
        public decimal Rate { get; set; }
        public decimal TotalCost => Hours * Rate;

        // Status
        public string Status { get; set; } // Enum mapped to string
        public bool IsApproved => Status == "Approved" || Status == "Billed";

        // Metadata
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}

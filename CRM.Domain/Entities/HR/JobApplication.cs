using CRM.Domain.Base;
using CRM.Domain.Enums.HumanResources;

namespace CRM.Domain.Entities.HR
{
    public class JobApplication : BaseEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string ResumePath { get; set; }
        public string CoverLetterPath { get; set; }
        public int JobPostingId { get; set; }
        public JobPosting JobPosting { get; set; }
        public ApplicationStatus Status { get; set; } // Enum: Received, UnderReview, Interview, Rejected, Hired
        public DateTime ApplicationDate { get; set; }
        public DateTime? InterviewDate { get; set; }
        public string Notes { get; set; }
    }
}

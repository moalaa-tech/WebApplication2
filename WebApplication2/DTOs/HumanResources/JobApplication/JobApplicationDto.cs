using CRM.Domain.Enums.HumanResources;

namespace CRM.WebApp.DTOs.HumanResources.JobApplication
{
    public class JobApplicationDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public string Email { get; set; }
        public string Phone { get; set; }
        public string ResumePath { get; set; }
        public string CoverLetterPath { get; set; }
        public int JobPostingId { get; set; }
        public string JobTitle { get; set; }
        public ApplicationStatus Status { get; set; }
        public string StatusName => Status.ToString();
        public DateTime ApplicationDate { get; set; }
        public DateTime? InterviewDate { get; set; }
        public string Notes { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.DTOs.HumanResources.JobPosting
{
    public class UpdateJobPostingDto
    {
        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public string Requirements { get; set; }
        public DateTime PostingDate { get; set; }
        public DateTime ClosingDate { get; set; }
        public bool IsActive { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public int ApplicationCount { get; set; }
    }
}

namespace CRM.WebApp.DTOs.HumanResources.JobPosting
{
    public class JobPostingDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

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

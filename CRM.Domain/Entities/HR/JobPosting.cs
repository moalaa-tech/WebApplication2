using CRM.Domain.Base;

namespace CRM.Domain.Entities.HR
{
    public class JobPosting : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Requirements { get; set; }
        public DateTime PostingDate { get; set; }
        public DateTime ClosingDate { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public ICollection<JobApplication> Applications { get; set; }
    }
}

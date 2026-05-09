using CRM.Domain.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.Domain.Entities.CustomerService
{
    public class SupportAgent : BaseEntity
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string JobTitle { get; set; }
        public string Department { get; set; }
        public DateTime HireDate { get; set; } = DateTime.UtcNow;
        public DateTime? TerminationDate { get; set; }
        public bool IsActive { get; set; } = true;
        public string TimeZone { get; set; } = "UTC";
        public string WorkingHours { get; set; } = "9:00 AM - 5:00 PM";
        // Skills and Specializations
        public List<string> Skills { get; set; } = new List<string>();
        public string Specialization { get; set; }
        // Authentication/Authorization
        public string AuthId { get; set; } // Reference to identity provider
        // Performance Metrics
        public int TotalTicketsResolved { get; set; }
        public double AverageRating { get; set; }
        public double AverageResolutionTimeHours { get; set; }

        // Navigation Properties
        public virtual ICollection<Ticket> AssignedTickets { get; set; } = new List<Ticket>();
        public virtual ICollection<ServiceRequest> AssignedServiceRequests { get; set; } = new List<ServiceRequest>();
        public virtual ICollection<KnowledgeBaseArticle> AuthoredArticles { get; set; } = new List<KnowledgeBaseArticle>();
        public virtual ICollection<KBArticleComment> ArticleComments { get; set; } = new List<KBArticleComment>();

        // SLA Relationships
        public virtual ICollection<ServiceLevelAgreement> ManagedSLAs { get; set; } = new List<ServiceLevelAgreement>();

        // Audit Fields
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime ModifiedDate { get; set; } = DateTime.UtcNow;
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }

        // Methods
        public bool IsAvailable()
        {
            return IsActive && TerminationDate == null;
        }
    }
}

using CRM.Domain.Base;

namespace CRM.Domain.Entities.CustomerService
{
    public class ServiceLevelAgreement : BaseEntity
    {
        public string Name { get; set; }
        public string NameAr { get; set; }
        public string Description { get; set; }
        public string ServiceType { get; set; }
        public int ResponseTime { get; set; } // in hours
        public int ResolutionTime { get; set; } // in hours
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModified { get; set; }
        public string EscalationProcess { get; set; }
        public string TermsAndConditions { get; set; }

        public int MetricsId { get; set; }

        public SLAMetrics Metrics { get; set; }
    }
}

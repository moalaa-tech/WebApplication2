namespace CRM.WebApp.ViewModels.CustomerService.ServiceLevelAgreement
{
    public class ServiceLevelAgreementViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ServiceType { get; set; }
        public int ResponseTime { get; set; }
        public int ResolutionTime { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModified { get; set; }
        public string EscalationProcess { get; set; }
        public string TermsAndConditions { get; set; }
        public SLAMetricsViewModel Metrics { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.ViewModels.CustomerService.ServiceLevelAgreement
{
    public class EditServiceLevelAgreementViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        [Required]
        [StringLength(100)]
        public string ServiceType { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int ResponseTime { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int ResolutionTime { get; set; }

        [StringLength(2000)]
        public string EscalationProcess { get; set; }

        [StringLength(4000)]
        public string TermsAndConditions { get; set; }
    }
}

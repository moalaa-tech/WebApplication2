using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.ViewModels.CustomerService
{
    public class CreateServiceRequestViewModel
    {
        [Required]
        [StringLength(50)]
        [Display(Name = "Request Type")]
        public string RequestType { get; set; }

        [Required]
        [StringLength(500)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Customer ID")]
        public int CustomerId { get; set; }
    }
}
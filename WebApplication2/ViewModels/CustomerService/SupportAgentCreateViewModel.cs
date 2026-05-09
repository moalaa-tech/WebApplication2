using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.ViewModels.CustomerService
{
    public class SupportAgentCreateViewModel
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Job Title")]
        public string JobTitle { get; set; }

        [Required]
        public string Department { get; set; }

        [Display(Name = "Specialization")]
        public string Specialization { get; set; }

        [Display(Name = "Working Hours")]
        public string WorkingHours { get; set; } = "9:00 AM - 5:00 PM";

        [Display(Name = "Time Zone")]
        public string TimeZone { get; set; } = "UTC";

        [Display(Name = "Skills (comma separated)")]
        public string Skills { get; set; }
    }
}

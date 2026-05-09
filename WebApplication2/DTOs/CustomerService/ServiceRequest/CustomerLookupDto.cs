using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.DTOs.CustomerService.ServiceRequest
{
    public class CustomerLookupDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Customer name cannot exceed 100 characters")]
        public string Name { get; set; }

        [StringLength(20, ErrorMessage = "Account number cannot exceed 20 characters")]
        public string AccountNumber { get; set; }

        [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters")]
        [EmailAddress(ErrorMessage = "Invalid email address format")]
        public string Email { get; set; }

        [StringLength(20, ErrorMessage = "Phone number cannot exceed 20 characters")]
        public string Phone { get; set; }

        public bool IsActive { get; set; }

        // For display purposes in dropdowns

        public string CustomerType { get; set; } // "Corporate", "Individual", etc.
        public string ServiceLevel { get; set; } // "Standard", "Premium", etc.
        public DateTime? ContractEndDate { get; set; }

        public string DisplayText =>
            $"{Name} ({AccountNumber}) - {CustomerType} {(ContractEndDate.HasValue ? $"| Contract ends: {ContractEndDate.Value:yyyy-MM-dd}" : "")}";


    }
}

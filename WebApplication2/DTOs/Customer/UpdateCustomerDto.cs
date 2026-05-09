using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.DTOs.Customer
{
    public class UpdateCustomerDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Customer name is required.")]
        [StringLength(100, ErrorMessage = "Customer name cannot exceed 100 characters.")]
        public string Name { get; set; }

        [StringLength(100, ErrorMessage = "Arabic customer name cannot exceed 100 characters.")]
        public string NameAr { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string? Description { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        [StringLength(250, ErrorMessage = "Email cannot exceed 250 characters.")]
        public string? Email { get; set; }

        [StringLength(500, ErrorMessage = "Address cannot exceed 500 characters.")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [Phone(ErrorMessage = "Invalid phone number format.")]
        [StringLength(20, ErrorMessage = "Phone number cannot exceed 20 characters.")]
        public string Phone { get; set; }
    }
}

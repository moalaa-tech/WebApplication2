using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.DTOs.Vendor
{
    public class UpdateVendorDto
    {
        [Required]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string ContactPerson { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string Phone { get; set; }

        public string Address { get; set; }
    }
}

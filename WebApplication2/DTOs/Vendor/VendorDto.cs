using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.DTOs.Vendor
{
    public class VendorDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ContactPerson { get; set; }

        [EmailAddress]
        public string Email { get; set; }


        [Phone]
        public string Phone { get; set; }
        public string? Address { get; set; }
    }
}

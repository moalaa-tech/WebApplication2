using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.DTOs.SupplyChainManagement
{
    public class AddressCreateDto
    {
        [Required, MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Company { get; set; }

        [Required, MaxLength(100)]
        public string Street1 { get; set; }

        [MaxLength(100)]
        public string Street2 { get; set; }

        [Required, MaxLength(50)]
        public string City { get; set; }

        [Required, MaxLength(50)]
        public string State { get; set; }

        [Required, MaxLength(20)]
        public string PostalCode { get; set; }

        [Required, MaxLength(2)]
        public string Country { get; set; }

        [MaxLength(20)]
        public string Phone { get; set; }

        [EmailAddress, MaxLength(100)]
        public string Email { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.DTOs.CustomerService
{
    public class CreateServiceRequestDto
    {
        [Required]
        [StringLength(50)]
        public string RequestType { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        public int CustomerId { get; set; }
    }
}
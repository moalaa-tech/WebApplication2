using CRM.WebApp.Attributes;
using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.DTOs.Deal
{
    // DTOs/CreateDealDTO.cs
    public class CreateDealDto
    {
        [Required]
        public int OpportunityId { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal FinalValue { get; set; }

        public string Description { get; set; }

        [Required]
        public string DealOwner { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string AccountName { get; set; }

        public bool Type { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Amount { get; set; }

        [Required]
        [FutureDate(ErrorMessage = "Closing date must be in the future")]
        public DateTime ClosingDate { get; set; }

        [Required]
        public int ContactId { get; set; }

        public List<IFormFile> Files { get; set; } = new List<IFormFile>();
    }
}

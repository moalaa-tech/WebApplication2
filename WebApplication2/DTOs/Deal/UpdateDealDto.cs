using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.DTOs.Deal
{
    // DTOs/UpdateDealDTO.cs
    public class UpdateDealDto
    {
        public int Id { get; set; }

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
        public DateTime ClosingDate { get; set; }

        [Required]
        public int ContactId { get; set; }

        public List<IFormFile> NewFiles { get; set; } = new List<IFormFile>();
        public List<int> FilesToDelete { get; set; } = new List<int>();
    }
}

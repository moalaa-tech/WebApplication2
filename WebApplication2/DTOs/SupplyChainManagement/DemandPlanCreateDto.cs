using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.DTOs.SupplyChainManagement
{
    public class DemandPlanCreateDto
    {
        [Required]
        [StringLength(100)]
        public string PlanName { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [StringLength(500)]
        public string Notes { get; set; }

        public ICollection<DemandPlanItemCreateDto> Items { get; set; }
    }
}

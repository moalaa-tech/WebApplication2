using CRM.WebApp.Attributes;
using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.ViewModels.SupplyChainManagement.DemandPlan
{
    public class DemandPlanCreateViewModel
    {
        [Required(ErrorMessage = "Plan name is required")]
        [StringLength(100, ErrorMessage = "Plan name cannot exceed 100 characters")]
        [Display(Name = "Plan Name")]
        public string PlanName { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Start date is required")]
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "End date is required")]
        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        [DateGreaterThan("StartDate", ErrorMessage = "End date must be after start date")]
        public DateTime EndDate { get; set; } = DateTime.Today.AddMonths(1);

        [Display(Name = "Plan Type")]
        public int PlanTypeId { get; set; }

        [Display(Name = "Business Unit")]
        public int? BusinessUnitId { get; set; }

        [Display(Name = "Region")]
        public int? RegionId { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; } = true;

        // Dropdown options for related entities
        public List<PlanTypeOption> PlanTypeOptions { get; set; } = new List<PlanTypeOption>();
        public List<BusinessUnitOption> BusinessUnitOptions { get; set; } = new List<BusinessUnitOption>();
        public List<RegionOption> RegionOptions { get; set; } = new List<RegionOption>();
    }

    public class PlanTypeOption
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class BusinessUnitOption
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class RegionOption
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}

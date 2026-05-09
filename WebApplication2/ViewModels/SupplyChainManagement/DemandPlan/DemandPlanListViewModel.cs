using CRM.WebApp.ViewModels.SalesManagement;

namespace CRM.WebApp.ViewModels.SupplyChainManagement.DemandPlan
{
    public class DemandPlanListViewModel
    {
        public List<DemandPlanListItem> Plans { get; set; } = new List<DemandPlanListItem>();
        public DemandPlanFilter Filters { get; set; } = new DemandPlanFilter();
        public PagingInfo PagingInfo { get; set; } = new PagingInfo();
        public SortOptions SortOptions { get; set; } = new SortOptions();

       
    }
    public class DemandPlanListItem
    {
        public int Id { get; set; }
        public string PlanName { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string PlanType { get; set; }
        public string BusinessUnit { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public int ItemCount { get; set; }
        public bool IsActive { get; set; }
    }

    public class DemandPlanFilter
    {
        public string PlanName { get; set; }
        public int? PlanTypeId { get; set; }
        public int? BusinessUnitId { get; set; }
        public DateTime? StartDateFrom { get; set; }
        public DateTime? StartDateTo { get; set; }
        public DateTime? EndDateFrom { get; set; }
        public DateTime? EndDateTo { get; set; }
        public bool? IsActive { get; set; }
        public string CreatedBy { get; set; }
    }

    public class PagingInfo
    {
        public int CurrentPage { get; set; } = 1;
        public int ItemsPerPage { get; set; } = 10;
        public int TotalItems { get; set; }
        public int TotalPages => (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);
    }

    public class SortOptions
    {
        public string SortBy { get; set; } = "StartDate";
        public bool SortDescending { get; set; } = true;
    }

}

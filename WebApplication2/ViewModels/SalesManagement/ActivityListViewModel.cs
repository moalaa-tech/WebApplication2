using CRM.WebApp.DTOs.SalesPipeline;
using CRM.WebApp.Paging;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CRM.WebApp.ViewModels.SalesManagement
{
    public class ActivityListViewModel
    {
        public PaginatedList<ActivityDto> Activities { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public ActivityFilterViewModel Filter { get; set; }
        public SelectList Users { get; set; }
    }
}

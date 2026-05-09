using CRM.WebApp.DTOs.Lead;

namespace CRM.WebApp.ViewModels.SalesManagement
{
    public class LeadListViewModel
    {
        public IEnumerable<LeadDto> Leads { get; set; }
        public LeadFilterViewModel Filter { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}

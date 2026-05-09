using Microsoft.AspNetCore.Mvc.Rendering;

namespace CRM.WebApp.ViewModels
{
    public class StateViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string NameAr { get; set; } = null!;
        public int CountryId { get; set; }
        public string? CountryName { get; set; }

        public IEnumerable<SelectListItem>? Countries { get; set; } // For dropdown in Create/Edit
    }

}

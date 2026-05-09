using System.ComponentModel.DataAnnotations;
using _DataType = System.ComponentModel.DataAnnotations.DataType;

namespace CRM.WebApp.ViewModels.SupplyChainManagement.ForecastData
{
    public class ForecastDataFilterViewModel
    {
        public string DataType { get; set; }

        [DataType(_DataType.Date)]
        public DateTime? StartDate { get; set; }

        [DataType(_DataType.Date)]
        public DateTime? EndDate { get; set; }

        public int? DemandPlanId { get; set; }

        public List<ForecastDataViewModel> Results { get; set; } = new List<ForecastDataViewModel>();
        public List<DropdownOption> DataTypes { get; set; }
        public List<DropdownOption> DemandPlans { get; set; }
    }
}

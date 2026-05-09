using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.ViewModels.SupplyChainManagement.Shipping
{
    public class TrackShipmentViewModel
    {
        [Required]
        public string TrackingNumber { get; set; }
    }
}

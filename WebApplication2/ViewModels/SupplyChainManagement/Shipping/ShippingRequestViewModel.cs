using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.ViewModels.SupplyChainManagement.Shipping
{
    public class ShippingRequestViewModel
    { // Origin Address
        [Required]
        public string OriginStreet { get; set; }

        [Required]
        public string OriginCity { get; set; }

        [Required]
        public string OriginState { get; set; }

        [Required]
        [DataType(DataType.PostalCode)]
        public string OriginZipCode { get; set; }

        [Required]
        public string OriginCountry { get; set; } = "US";

        // Destination Address
        [Required]
        public string DestinationStreet { get; set; }

        [Required]
        public string DestinationCity { get; set; }

        [Required]
        public string DestinationState { get; set; }

        [Required]
        [DataType(DataType.PostalCode)]
        public string DestinationZipCode { get; set; }

        [Required]
        public string DestinationCountry { get; set; } = "US";

        // Package Information
        public List<PackageViewModel> Packages { get; set; } = new List<PackageViewModel>();

        // Carrier Selection
        public string CarrierId { get; set; }
        public List<SelectListItem> AvailableCarriers { get; set; } = new List<SelectListItem>();
    }
}

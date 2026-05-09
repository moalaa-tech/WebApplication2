using CRM.Domain.Entities.SupplyChainManagement;
using CRM.Domain.Requests;

namespace CRM.WebApp.Services.SupplyChainManagement
{
    public interface IShippingService
    {
        Task<ShippingResult> CalculateShippingAsync(ShippingRequest request);
        Task<ShippingQuote> GetShippingQuoteAsync(ShippingQuoteRequest request);
        Task<IEnumerable<ShippingOption>> GetAvailableShippingOptionsAsync(ShippingOptionsRequest request);
        Task<ShipmentTrackingInfo> GetTrackingInfoAsync(string trackingNumber);
        Task<ShippingLabel> CreateShippingLabelAsync(ShippingLabelRequest request);
        Task<bool> CancelShipmentAsync(string shipmentId);
        Task<IEnumerable<ShippingCarrier>> GetSupportedCarriersAsync();
    }
}

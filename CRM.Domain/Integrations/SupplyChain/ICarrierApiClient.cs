using CRM.Domain.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Domain.Integrations.SupplyChain
{
    public interface ICarrierApiClient
    {
        Task<CarrierRateResponse> GetRatesAsync(ShippingRequest request);
        Task<IEnumerable<ShippingOption>> GetShippingOptionsAsync(ShippingOptionsRequest request);
        Task<ShipmentTrackingInfo> GetTrackingInfoAsync(string trackingNumber);
        Task<ShippingLabel> CreateLabelAsync(ShippingLabelRequest request);
        Task<bool> CancelShipmentAsync(string shipmentId);
        Task<bool> ValidateAddressAsync(Address address);
        Task<CarrierAccountInfo> GetAccountInfoAsync();
    }
}

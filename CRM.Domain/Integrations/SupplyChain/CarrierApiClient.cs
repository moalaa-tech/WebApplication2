using CRM.Domain.Requests;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CRM.Domain.Integrations.SupplyChain
{
    public class CarrierApiClient : ICarrierApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly CarrierApiSettings _settings;
        private readonly ILogger<CarrierApiClient> _logger;

        public CarrierApiClient(
            HttpClient httpClient,
            IOptions<CarrierApiSettings> settings,
            ILogger<CarrierApiClient> logger)
        {
            _httpClient = httpClient;
            _settings = settings.Value;
            _logger = logger;

            ConfigureHttpClient();
        }



        public async Task<CarrierRateResponse> GetRatesAsync(ShippingRequest request)
        {
            try
            {
                var rateRequest = new
                {
                    origin = MapAddress(request.Origin),
                    destination = MapAddress(request.Destination),
                    //packages = request.Packages.Select(MapPackage),
                    ship_date = request.ShipDate?.ToString("yyyy-MM-dd")
                };

                var response = await _httpClient.PostAsJsonAsync("rates", rateRequest);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var rateResponse = JsonSerializer.Deserialize<CarrierRateApiResponse>(content);

                return new CarrierRateResponse
                {
                    Success = true,
                    TotalCost = rateResponse.TotalCharge,
                    EstimatedDeliveryDate = rateResponse.EstimatedDeliveryDate,
                    ServiceName = rateResponse.ServiceType,
                    TrackingNumber = rateResponse.TrackingNumber,
                    CarrierServiceId = rateResponse.ServiceCode
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting rates from carrier API");
                return new CarrierRateResponse
                {
                    Success = false,
                    ErrorMessage = "Failed to get shipping rates"
                };
            }
        }

        public async Task<IEnumerable<ShippingOption>> GetShippingOptionsAsync(ShippingOptionsRequest request)
        {
            try
            {
                var optionsRequest = new
                {
                    from_postal_code = request.Origin?.PostalCode,
                    to_postal_code = request.Destination?.PostalCode,
                    total_weight = request.TotalWeight,
                    total_value = request.TotalValue
                };

                var response = await _httpClient.PostAsJsonAsync("options", optionsRequest);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var optionsResponse = JsonSerializer.Deserialize<CarrierOptionsResponse>(content);

                return optionsResponse.Services.Select(s => new ShippingOption
                {
                    CarrierId = _settings.CarrierId,
                    CarrierName = _settings.CarrierName,
                    ServiceId = s.ServiceCode,
                    ServiceName = s.ServiceName,
                    Cost = s.Cost,
                    EstimatedDeliveryDate = DateTime.UtcNow.AddDays(s.TransitDays),
                    TransitDays = s.TransitDays,
                    RequiresSignature = s.RequiresSignature,
                    IsInsured = s.IncludesInsurance,
                    MaximumInsuranceValue = s.MaxInsuranceValue
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting shipping options from carrier API");
                return Enumerable.Empty<ShippingOption>();
            }
        }

        public async Task<ShipmentTrackingInfo> GetTrackingInfoAsync(string trackingNumber)
        {
            try
            {
                var response = await _httpClient.GetAsync($"tracking/{trackingNumber}");
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var trackingResponse = JsonSerializer.Deserialize<CarrierTrackingResponse>(content);

                return new ShipmentTrackingInfo
                {
                    TrackingNumber = trackingNumber,
                    Carrier = _settings.CarrierName,
                    Status = trackingResponse.Status,
                    CurrentStatus = trackingResponse.CurrentStatus,
                    EstimatedDeliveryDate = trackingResponse.EstimatedDelivery,
                    ActualDeliveryDate = trackingResponse.DeliveryDate,
                    Events = trackingResponse.Events.Select(e => new TrackingEvent
                    {
                        Timestamp = e.Timestamp,
                        Location = e.Location,
                        Description = e.Description,
                        Status = e.Status,
                        EventCode = e.EventCode
                    }).ToArray()
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting tracking info for {TrackingNumber}", trackingNumber);
                return null;
            }
        }

        public async Task<ShippingLabel> CreateLabelAsync(ShippingLabelRequest request)
        {
            try
            {
                var labelRequest = new
                {
                    service_code = request.CarrierServiceId,
                    from_address = MapAddress(request.Origin),
                    to_address = MapAddress(request.Destination),
                   // packages = request.Packages.Select(MapPackage),
                    reference = request.CustomerReference
                };

                var response = await _httpClient.PostAsJsonAsync("labels", labelRequest);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var labelResponse = JsonSerializer.Deserialize<CarrierLabelResponse>(content);

                return new ShippingLabel
                {
                    LabelData = Convert.FromBase64String(labelResponse.LabelBase64),
                    FileFormat = labelResponse.FileFormat,
                    TrackingNumber = labelResponse.TrackingNumber,
                    LabelUrl = labelResponse.LabelUrl
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating shipping label");
                throw;
            }
        }

        public async Task<bool> CancelShipmentAsync(string shipmentId)
        {
            try
            {
                var response = await _httpClient.PostAsync($"shipments/{shipmentId}/cancel", null);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error cancelling shipment {ShipmentId}", shipmentId);
                return false;
            }
        }

        public async Task<bool> ValidateAddressAsync(Address address)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("address/validate", MapAddress(address));
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error validating address");
                return false;
            }
        }

        public async Task<CarrierAccountInfo> GetAccountInfoAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("account");
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<CarrierAccountInfo>(content);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting account info");
                return null;
            }
        }

        private void ConfigureHttpClient()
        {
            _httpClient.BaseAddress = new Uri(_settings.BaseUrl);
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_settings.ApiKey}");
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        private object MapAddress(Address address) => address == null ? null : new
        {
            name = address.Name,
            company = address.Company,
            street1 = address.Street1,
            street2 = address.Street2,
            city = address.City,
            state = address.State,
            postal_code = address.PostalCode,
            country = address.Country,
            phone = address.Phone,
            email = address.Email
        };

        //private object MapPackage(ShippingPackageDto package) => new
        //{
        //    weight = package.Weight,
        //    weight_unit = package.WeightUnit,
        //    length = package.Length,
        //    width = package.Width,
        //    height = package.Height,
        //    dimension_unit = package.DimensionUnit,
        //    description = package.Description,
        //    value = package.Value,
        //    currency = package.Currency
        //};
    }
}

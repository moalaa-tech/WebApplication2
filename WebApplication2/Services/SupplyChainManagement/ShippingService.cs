using CRM.Domain.Entities.SupplyChainManagement;
using CRM.Domain.Enums.SupplyChainManagement;
using CRM.Domain.Integrations.SupplyChain;
using CRM.Domain.Requests;
using CRM.WebApp.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CRM.WebApp.Services.SupplyChainManagement
{
    public class ShippingService : IShippingService
    {
        private readonly ICarrierApiClient _carrierApiClient;
        private readonly IRepository<Shipping> _shippingRepository;
        private readonly IRepository<ShippingCarrier> ShippingCarrierRepository;
        private readonly IRepository<LabelRecord> LabelRecordRepository;

        private readonly ILogger<ShippingService> _logger;

        public ShippingService(
            ICarrierApiClient carrierApiClient,
            IRepository<Shipping> shippingRepository,
            IRepository<ShippingCarrier> _ShippingCarrierRepository,
            IRepository<LabelRecord> _LabelRecordRepository,
            ILogger<ShippingService> logger)
        {
            _carrierApiClient = carrierApiClient;
            _shippingRepository = shippingRepository;
            _logger = logger;
            ShippingCarrierRepository = _ShippingCarrierRepository;
            LabelRecordRepository = _LabelRecordRepository;
        }



        public async Task<ShippingResult> CalculateShippingAsync(ShippingRequest request)
        {
            try
            {
                ValidateShippingRequest(request);

                var response = await _carrierApiClient.GetRatesAsync(request);

                if (!response.Success)
                {
                    return new ShippingResult
                    {
                        Success = false,
                        ErrorMessage = response.ErrorMessage
                    };
                }

                return new ShippingResult
                {
                    Success = true,
                    TotalCost = response.TotalCost,
                    EstimatedDeliveryDate = response.EstimatedDeliveryDate,
                    CarrierServiceName = response.ServiceName,
                    TrackingNumber = response.TrackingNumber
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calculating shipping");
                return new ShippingResult
                {
                    Success = false,
                    ErrorMessage = "An error occurred while calculating shipping"
                };
            }
        }

        public async Task<ShippingQuote> GetShippingQuoteAsync(ShippingQuoteRequest request)
        {
            var result = await CalculateShippingAsync(request);

            return new ShippingQuote
            {
                Success = result.Success,
                TotalCost = result.TotalCost,
                EstimatedDeliveryDate = result.EstimatedDeliveryDate,
                CarrierServiceName = result.CarrierServiceName,
                TrackingNumber = result.TrackingNumber,
                ErrorMessage = result.ErrorMessage,
                QuoteId = Guid.NewGuid().ToString(),
                QuoteExpiration = DateTime.UtcNow.AddHours(24)
            };
        }

        public async Task<IEnumerable<ShippingOption>> GetAvailableShippingOptionsAsync(ShippingOptionsRequest request)
        {
            var options = await _carrierApiClient.GetShippingOptionsAsync(request);
            return options.OrderBy(o => o.EstimatedDeliveryDate)
                          .ThenBy(o => o.Cost);
        }

        public async Task<ShipmentTrackingInfo> GetTrackingInfoAsync(string trackingNumber)
        {
            if (string.IsNullOrWhiteSpace(trackingNumber))
                throw new ArgumentException("Tracking number is required");

            return await _carrierApiClient.GetTrackingInfoAsync(trackingNumber);
        }

        public async Task<ShippingLabel> CreateShippingLabelAsync(ShippingLabelRequest request)
        {
            ValidateShippingLabelRequest(request);

            var label = await _carrierApiClient.CreateLabelAsync(request);

           var lblRecord = new LabelRecord
            {
                TrackingNumber = label.TrackingNumber,
                DateCreated = DateTime.UtcNow,
                LabelData = label.LabelData,
                CarrierService = request.CarrierServiceId
            };

            await LabelRecordRepository.AddAsync(lblRecord);
            await LabelRecordRepository.SaveChangesAsync();

            return label;
        }

        public async Task<bool> CancelShipmentAsync(string shipmentId)
        {
            if (string.IsNullOrWhiteSpace(shipmentId))
                return false;

            try
            {
                var success = await _carrierApiClient.CancelShipmentAsync(shipmentId);
                if (success)
                {
                    // await _shippingRepository.UpdateShipmentStatusAsync(shipmentId, "Cancelled");
                    var shipment = await _shippingRepository.GetAll()
                    .FirstOrDefaultAsync(s => s.TrackingNumber == shipmentId);

                    if (shipment == null) return false;

                    shipment.Status = ShippingStatus.Cancelled;
                    shipment.DateModified = DateTime.UtcNow;

                    if (shipment.Status == ShippingStatus.Delivered)
                    {
                        shipment.ShippingDate = DateTime.UtcNow;
                    }
                    await _shippingRepository.SaveChangesAsync();
                    return true; ;
                }
                return success;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error cancelling shipment {shipmentId}");
                return false;
            }
        }

        public async Task<IEnumerable<ShippingCarrier>> GetSupportedCarriersAsync()
        {
            return await ShippingCarrierRepository.GetAll()
                .Where(c => c.IsActive)
                .OrderBy(c => c.Name)
                .ToListAsync();
        }

        private void ValidateShippingRequest(ShippingRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            //if (request.Packages == null || !request.Packages.Any())
            //    throw new ArgumentException("At least one package is required");

            if (request.Destination == null)
                throw new ArgumentException("Destination address is required");
        }

        private void ValidateShippingLabelRequest(ShippingLabelRequest request)
        {
            ValidateShippingRequest(request);

            if (string.IsNullOrWhiteSpace(request.CarrierServiceId))
                throw new ArgumentException("Carrier service ID is required");
        }
    }
}

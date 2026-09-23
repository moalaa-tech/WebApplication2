using CRM.Domain.Entities.SupplyChainManagement;
using CRM.Domain.Integrations.SupplyChain;
using CRM.Domain.Requests;
using CRM.WebApp.Services.SupplyChainManagement;
using CRM.WebApp.ViewModels.SupplyChainManagement.Shipping;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Api.Controllers.SupplyChainManagement
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShippingController : ControllerBase
    {
        private readonly IShippingService _shippingService;
        private readonly ILogger<ShippingController> _logger;

        public ShippingController(IShippingService shippingService, ILogger<ShippingController> logger)
        {
            _shippingService = shippingService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var carriers = await _shippingService.GetSupportedCarriersAsync();
                return Ok(carriers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading shipping index");
                return Ok(new List<ShippingCarrier>());
            }
        }

        [HttpGet("Calculate")]
        public async Task<IActionResult> Calculate()
        {
            var model = new ShippingRequestViewModel();
            await PopulateCarrierDropdowns(model);
            return Ok(model);
        }

        [HttpPost("Calculate")]
        public async Task<IActionResult> Calculate([FromBody] ShippingRequestViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var request = MapToShippingRequest(model);
                var result = await _shippingService.CalculateShippingAsync(request);

                if (result.Success)
                {
                    var quoteViewModel = new ShippingQuoteViewModel
                    {
                        TotalCost = result.TotalCost,
                        EstimatedDeliveryDate = result.EstimatedDeliveryDate,
                        CarrierServiceName = result.CarrierServiceName,
                        TrackingNumber = result.TrackingNumber
                    };

                    return Ok(quoteViewModel);
                }
                else
                {
                    return BadRequest(new { message = result.ErrorMessage });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calculating shipping");
                return BadRequest(new { message = "An error occurred while calculating shipping" });
            }
        }

        [HttpGet("Quote")]
        public async Task<IActionResult> Quote()
        {
            var model = new ShippingQuoteRequestViewModel();
            await PopulateCarrierDropdowns(model);
            return Ok(model);
        }

        [HttpPost("Quote")]
        public async Task<IActionResult> Quote([FromBody] ShippingQuoteRequestViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var request = MapToShippingQuoteRequest(model);
                var quote = await _shippingService.GetShippingQuoteAsync(request);

                if (quote.Success)
                {
                    var quoteViewModel = new ShippingQuoteViewModel
                    {
                        QuoteId = quote.QuoteId,
                        TotalCost = quote.TotalCost,
                        EstimatedDeliveryDate = quote.EstimatedDeliveryDate,
                        CarrierServiceName = quote.CarrierServiceName,
                        TrackingNumber = quote.TrackingNumber,
                        QuoteExpiration = quote.QuoteExpiration
                    };

                    return Ok(quoteViewModel);
                }
                else
                {
                    return BadRequest(new { message = quote.ErrorMessage });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting shipping quote");
                return BadRequest(new { message = "An error occurred while getting shipping quote" });
            }
        }

        [HttpGet("Options")]
        public async Task<IActionResult> Options()
        {
            var model = new ShippingOptionsRequestViewModel();
            await PopulateCarrierDropdowns(model);
            return Ok(model);
        }

        [HttpPost("Options")]
        public async Task<IActionResult> Options([FromBody] ShippingOptionsRequestViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var request = MapToShippingOptionsRequest(model);
                var options = await _shippingService.GetAvailableShippingOptionsAsync(request);

                return Ok(options.ToList());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting shipping options");
                return BadRequest(new { message = "An error occurred while getting shipping options" });
            }
        }

        [HttpGet("Track")]
        public IActionResult Track()
        {
            return Ok(new TrackShipmentViewModel());
        }

        [HttpPost("Track")]
        public async Task<IActionResult> Track([FromBody] TrackShipmentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var trackingInfo = await _shippingService.GetTrackingInfoAsync(model.TrackingNumber);
                return Ok(trackingInfo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error tracking shipment");
                return BadRequest(new { message = "An error occurred while tracking the shipment" });
            }
        }

        [HttpGet("CreateLabel")]
        public async Task<IActionResult> CreateLabel()
        {
            var model = new ShippingLabelRequestViewModel();
            await PopulateCarrierDropdowns(model);
            return Ok(model);
        }

        [HttpPost("CreateLabel")]
        public async Task<IActionResult> CreateLabel([FromBody] ShippingLabelRequestViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var request = MapToShippingLabelRequest(model);
                var label = await _shippingService.CreateShippingLabelAsync(request);

                return Ok(label);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating shipping label");
                return BadRequest(new { message = "An error occurred while creating shipping label" });
            }
        }

        [HttpPost("Cancel")]
        public async Task<IActionResult> Cancel([FromQuery] string trackingNumber)
        {
            if (string.IsNullOrWhiteSpace(trackingNumber))
            {
                return BadRequest(new { message = "Tracking number is required" });
            }

            try
            {
                var success = await _shippingService.CancelShipmentAsync(trackingNumber);
                if (success)
                {
                    return Ok(new { success = true, message = $"Shipment {trackingNumber} cancelled successfully" });
                }
                else
                {
                    return Ok(new { success = false, message = $"Failed to cancel shipment {trackingNumber}" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error cancelling shipment {trackingNumber}");
                return BadRequest(new { success = false, message = $"Error cancelling shipment {trackingNumber}" });
            }
        }

        private async Task PopulateCarrierDropdowns(object model)
        {
            var carriers = await _shippingService.GetSupportedCarriersAsync();
            var carrierList = carriers.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToList();

            if (model is ShippingRequestViewModel shippingModel)
            {
                shippingModel.AvailableCarriers = carrierList;
            }
            else if (model is ShippingQuoteRequestViewModel quoteModel)
            {
                quoteModel.AvailableCarriers = carrierList;
            }
            else if (model is ShippingOptionsRequestViewModel optionsModel)
            {
                optionsModel.AvailableCarriers = carrierList;
            }
            else if (model is ShippingLabelRequestViewModel labelModel)
            {
                labelModel.AvailableCarriers = carrierList;
            }
        }

        private ShippingRequest MapToShippingRequest(ShippingRequestViewModel model)
        {
            return new ShippingRequest
            {
                Origin = new Address
                {
                    Street1 = model.OriginStreet,
                    City = model.OriginCity,
                    State = model.OriginState,
                    PostalCode = model.OriginZipCode,
                    Country = model.OriginCountry
                },
                Destination = new Address
                {
                    Street1 = model.DestinationStreet,
                    City = model.DestinationCity,
                    State = model.DestinationState,
                    PostalCode = model.DestinationZipCode,
                    Country = model.DestinationCountry
                },

                //Packages = model.Packages?.Select(p => new Package
                //{
                //    Weight = p.Weight,
                //    Length = p.Length,
                //    Width = p.Width,
                //    Height = p.Height
                //}).ToList(),
                //CarrierId = model.CarrierId
            };
        }

        private ShippingQuoteRequest MapToShippingQuoteRequest(ShippingQuoteRequestViewModel model)
        {
            return new ShippingQuoteRequest
            {
                Origin = new Address
                {
                    Street1 = model.OriginStreet,
                    City = model.OriginCity,
                    State = model.OriginState,
                    PostalCode = model.OriginZipCode,
                    Country = model.OriginCountry
                },
                Destination = new Address
                {
                    Street1 = model.DestinationStreet,
                    City = model.DestinationCity,
                    State = model.DestinationState,
                    PostalCode = model.DestinationZipCode,
                    Country = model.DestinationCountry
                },
                //Packages = model.Packages?.Select(p => new Package
                //{
                //    Weight = p.Weight,
                //    Length = p.Length,
                //    Width = p.Width,
                //    Height = p.Height
                //}).ToList(),
                //CarrierId = model.CarrierId
            };
        }

        private ShippingOptionsRequest MapToShippingOptionsRequest(ShippingOptionsRequestViewModel model)
        {
            return new ShippingOptionsRequest
            {
                Origin = new Address
                {
                    Street1 = model.OriginStreet,
                    City = model.OriginCity,
                    State = model.OriginState,
                    PostalCode = model.OriginZipCode,
                    Country = model.OriginCountry
                },
                Destination = new Address
                {
                    Street1 = model.DestinationStreet,
                    City = model.DestinationCity,
                    State = model.DestinationState,
                    PostalCode = model.DestinationZipCode,
                    Country = model.DestinationCountry
                },
                //Packages = model.Packages?.Select(p => new Package
                //{
                //    Weight = p.Weight,
                //    Length = p.Length,
                //    Width = p.Width,
                //    Height = p.Height
                //}).ToList()
            };
        }

        private ShippingLabelRequest MapToShippingLabelRequest(ShippingLabelRequestViewModel model)
        {
            return new ShippingLabelRequest
            {
                Origin = new Address
                {
                    Street1 = model.OriginStreet,
                    City = model.OriginCity,
                    State = model.OriginState,
                    PostalCode = model.OriginZipCode,
                    Country = model.OriginCountry
                },
                Destination = new Address
                {
                    Street1 = model.DestinationStreet,
                    City = model.DestinationCity,
                    State = model.DestinationState,
                    PostalCode = model.DestinationZipCode,
                    Country = model.DestinationCountry
                },
                //Packages = model.Packages?.Select(p => new Package
                //{
                //    Weight = p.Weight,
                //    Length = p.Length,
                //    Width = p.Width,
                //    Height = p.Height
                //}).ToList(),
                //CarrierServiceId = model.CarrierServiceId,
                //ShipDate = model.ShipDate
            };
        }
    }
}
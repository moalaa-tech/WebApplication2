using CRM.Domain.Entities.SupplyChainManagement;
using CRM.Domain.Integrations.SupplyChain;
using CRM.Domain.Requests;
using CRM.WebApp.Services.SupplyChainManagement;
using CRM.WebApp.ViewModels.SupplyChainManagement.Shipping;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.Controllers.SupplyChainManagement
{
    public class ShippingController : Controller
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
                return View(carriers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading shipping index");
                TempData["ErrorMessage"] = "Error loading shipping information";
                return View(new List<ShippingCarrier>());
            }
        }

        [HttpGet]
        public async Task<IActionResult> Calculate()
        {
            var model = new ShippingRequestViewModel();
            await PopulateCarrierDropdowns(model);
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Calculate(ShippingRequestViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await PopulateCarrierDropdowns(model);
                return View(model);
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

                    return View("QuoteResult", quoteViewModel);
                }
                else
                {
                    ModelState.AddModelError("", result.ErrorMessage);
                    await PopulateCarrierDropdowns(model);
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calculating shipping");
                ModelState.AddModelError("", "An error occurred while calculating shipping");
                await PopulateCarrierDropdowns(model);
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Quote()
        {
            var model = new ShippingQuoteRequestViewModel();
            await PopulateCarrierDropdowns(model);
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Quote(ShippingQuoteRequestViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await PopulateCarrierDropdowns(model);
                return View(model);
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

                    return View("QuoteResult", quoteViewModel);
                }
                else
                {
                    ModelState.AddModelError("", quote.ErrorMessage);
                    await PopulateCarrierDropdowns(model);
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting shipping quote");
                ModelState.AddModelError("", "An error occurred while getting shipping quote");
                await PopulateCarrierDropdowns(model);
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Options()
        {
            var model = new ShippingOptionsRequestViewModel();
            await PopulateCarrierDropdowns(model);
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Options(ShippingOptionsRequestViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await PopulateCarrierDropdowns(model);
                return View(model);
            }

            try
            {
                var request = MapToShippingOptionsRequest(model);
                var options = await _shippingService.GetAvailableShippingOptionsAsync(request);

                return View("ShippingOptions", options.ToList());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting shipping options");
                ModelState.AddModelError("", "An error occurred while getting shipping options");
                await PopulateCarrierDropdowns(model);
                return View(model);
            }
        }


        [HttpGet]
        public IActionResult Track()
        {
            return View(new TrackShipmentViewModel());
        }

        // POST: Shipping/Track
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Track(TrackShipmentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var trackingInfo = await _shippingService.GetTrackingInfoAsync(model.TrackingNumber);
                return View("TrackingInfo", trackingInfo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error tracking shipment");
                ModelState.AddModelError("", "An error occurred while tracking the shipment");
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> CreateLabel()
        {
            var model = new ShippingLabelRequestViewModel();
            await PopulateCarrierDropdowns(model);
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateLabel(ShippingLabelRequestViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await PopulateCarrierDropdowns(model);
                return View(model);
            }

            try
            {
                var request = MapToShippingLabelRequest(model);
                var label = await _shippingService.CreateShippingLabelAsync(request);

                return View("LabelCreated", label);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating shipping label");
                ModelState.AddModelError("", "An error occurred while creating shipping label");
                await PopulateCarrierDropdowns(model);
                return View(model);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cancel(string trackingNumber)
        {
            if (string.IsNullOrWhiteSpace(trackingNumber))
            {
                TempData["ErrorMessage"] = "Tracking number is required";
                return RedirectToAction(nameof(Index));
            }

            try
            {
                var success = await _shippingService.CancelShipmentAsync(trackingNumber);
                if (success)
                {
                    TempData["SuccessMessage"] = $"Shipment {trackingNumber} cancelled successfully";
                }
                else
                {
                    TempData["ErrorMessage"] = $"Failed to cancel shipment {trackingNumber}";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error cancelling shipment {trackingNumber}");
                TempData["ErrorMessage"] = $"Error cancelling shipment {trackingNumber}";
            }

            return RedirectToAction(nameof(Index));
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
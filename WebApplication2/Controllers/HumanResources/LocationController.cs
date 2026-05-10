using CRM.WebApp.Services.Interfaces;
using CRM.WebApp.Services.Lookups;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebApp.Controllers.HumanResources
{
    [AllowAnonymous]
    public class LocationController : Controller
    {
        public readonly IStateService _stateService;
        public readonly ICountryService _countryService;

        public LocationController(
            IStateService stateService,
            ICountryService countryService)
        {
            _stateService = stateService;
            _countryService = countryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var countries = await _countryService.GetAllAsync();
            var states = await _stateService.GetAllAsync();

            ViewBag.Countries = countries;
            ViewBag.States = states;

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetStatesByCountry(int countryId)
        {
            var states = await _stateService.GetStatesByCountryIdAsync(countryId);
            return Json(states);
        }

        [HttpGet]
        public async Task<IActionResult> GetCitiesByState(int stateId)
        {
            var cities = await _stateService.GetCitiesByStateIdAsync(stateId);
            return Json(cities);
        }
    }
}

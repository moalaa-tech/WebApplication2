using CRM.WebApp.Services.Lookups;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Api.Controllers.HumanResources
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class LocationController : ControllerBase
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

            return Ok(new { Countries = countries, States = states });
        }

        [HttpGet("GetStatesByCountry")]
        public async Task<IActionResult> GetStatesByCountry([FromQuery] int countryId)
        {
            var states = await _stateService.GetStatesByCountryIdAsync(countryId);
            return Ok(states);
        }

        [HttpGet("GetCitiesByState")]
        public async Task<IActionResult> GetCitiesByState([FromQuery] int stateId)
        {
            var cities = await _stateService.GetCitiesByStateIdAsync(stateId);
            return Ok(cities);
        }
    }
}

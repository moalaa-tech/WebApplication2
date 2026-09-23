using AutoMapper;
using CRM.WebApp.DTOs.HumanResources;
using CRM.WebApp.Services.Lookups;
using CRM.WebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication2.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LookupController : ControllerBase
    {
        private readonly ICountryService CountryService;
        private readonly IMapper _Mapper;
        private readonly IStateService StateService;

        public LookupController(ICountryService _CountryService, IMapper mapper, IStateService _StateService)
        {
            StateService = _StateService;
            _Mapper = mapper;
            CountryService = _CountryService;
        }

        [HttpGet("CountriesIndex")]
        public async Task<IActionResult> CountriesIndex()
        {
            var countries = await CountryService.GetAllAsync();
            return Ok(countries);
        }

        [HttpPost("CreateCountry")]
        public async Task<IActionResult> CreateCountry([FromBody] CountryViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await CountryService.CreateAsync(_Mapper.Map<CountryDto>(model));
            return Ok();
        }


        [HttpPut("EditCountry")]
        public async Task<IActionResult> EditCountry([FromBody] CountryViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await CountryService.UpdateAsync(_Mapper.Map<CountryDto>(model));
            return Ok();
        }

        [HttpDelete("DeleteCountry/{id:int}")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            await CountryService.DeleteAsync(id);
            return Ok();
        }


        [HttpGet("StatesIndex")]
        public async Task<IActionResult> StatesIndex()
        {
            var states = await StateService.GetAllAsync();
            return Ok(states);
        }

        [HttpPost("CreateState")]
        public async Task<IActionResult> CreateState([FromBody] StateViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await StateService.CreateAsync(_Mapper.Map<StateDto>(model));
            return Ok();
        }

        [HttpPut("EditState")]
        public async Task<IActionResult> EditState([FromBody] StateViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await StateService.UpdateAsync(_Mapper.Map<StateDto>(model));
            return Ok();
        }

        [HttpDelete("DeleteState/{id:int}")]
        public async Task<IActionResult> DeleteState(int id)
        {
            await StateService.DeleteAsync(id);
            return Ok();
        }

        [HttpGet("GetCitiesByStateId/{id:int}")]
        public async Task<IActionResult> GetCitiesByStateId(int id)
        {
            var cities = await StateService.GetCitiesByStateIdAsync(id);
            return Ok(cities);
        }
    }
}
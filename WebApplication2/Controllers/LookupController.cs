using AutoMapper;
using CRM.WebApp.DTOs.HumanResources;
using CRM.WebApp.Services.Lookups;
using CRM.WebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ERP.WebApp.Controllers
{
    public class LookupController : Controller
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

        [HttpGet]
        public async Task<IActionResult> CountriesIndex()
        {
            var countries = await CountryService.GetAllAsync();
            return View(countries);
        }

        [HttpGet]
        public IActionResult CreateCountry() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCountry(CountryViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            await CountryService.CreateAsync(_Mapper.Map<CountryDto>(model));
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> EditCountry(int id)
        {
            var dto = await CountryService.GetByIdAsync(id);
            if (dto == null) return NotFound();

            return View(_Mapper.Map<CountryViewModel>(dto));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCountry(CountryViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            await CountryService.UpdateAsync(_Mapper.Map<CountryDto>(model));
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            await CountryService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> StatesIndex()
        {
            var states = await StateService.GetAllAsync();
            return View(states);
        }

        [HttpGet]
        public async Task<IActionResult> CreateState()
        {
            var countries = await CountryService.GetAllAsync();
            var model = new StateViewModel
            {
                Countries = countries.Select(c => new SelectListItem(c.Name, c.Id.ToString()))
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateState(StateViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            await StateService.CreateAsync(_Mapper.Map<StateDto>(model));
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> EditState(int id)
        {
            var dto = await StateService.GetByIdAsync(id);
            if (dto == null) return NotFound();

            var countries = await CountryService.GetAllAsync();
            var model = _Mapper.Map<StateViewModel>(dto);
            model.Countries = countries.Select(c => new SelectListItem(c.Name, c.Id.ToString()));

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditState(StateViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            await StateService.UpdateAsync(_Mapper.Map<StateDto>(model));
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteState(int id)
        {
            await StateService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}

using AutoMapper;
using CRM.Identity.Services;
using CRM.WebApp.DTOs.Company;
using CRM.WebApp.Services;
using CRM.WebApp.Services.Interfaces;
using CRM.WebApp.ViewModels.Company;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication2.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        private readonly IUserService _userService;
        private readonly IBusinessService _BusinessService;
        private readonly IMapper _mapper;
        public CompanyController(ICompanyService companyService, IUserService userService, IBusinessService businessService, IMapper mapper)
        {
            _companyService = companyService;
            _userService = userService;
            _BusinessService = businessService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var companies = await _companyService.GetAllCompaniesAsync();
            var viewModel = _mapper.Map<IEnumerable<CompanyViewModel>>(companies);
            return Ok(viewModel);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = await _companyService.GetCompanyByIdAsync(id.Value);
            if (company == null)
            {
                return NotFound();
            }
            return Ok(company);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCompanyViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var companyCreateDto = _mapper.Map<CreateCompanyDto>(viewModel);
                var createdCompany = await _companyService.CreateCompanyAsync(companyCreateDto);
                if (createdCompany != null)
                {
                    return Ok();
                }
                return BadRequest(new { message = "Could not create company." });
            }

            var bs = await _BusinessService.GetAllBusinesessAsync();
            viewModel.Businesses = bs.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name,
            }).ToList();

            var users = await _userService.GetAllUsersAsync();
            viewModel.Users = users.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name,
            }).ToList();

            return Ok(viewModel);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Edit(int id, [FromBody] UpdateCompanyDto companyUpdateDto)
        {
            if (id != companyUpdateDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var updatedCompany = await _companyService.UpdateCompanyAsync(companyUpdateDto);
                if (updatedCompany == true)
                {
                    return Ok();
                }
                return BadRequest(new { message = "Could not update company. It might not exist or is already deleted." });
            }
            return Ok(companyUpdateDto);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _companyService.DeleteCompanyAsync(id);
            return Ok();
        }
    }
}
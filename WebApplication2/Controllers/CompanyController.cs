using AutoMapper;
using CRM.Identity.Services;
using CRM.WebApp.DTOs.Company;
using CRM.WebApp.Services;
using CRM.WebApp.Services.Interfaces;
using CRM.WebApp.ViewModels.Company;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace CRM.WebApp.Controllers
{
    public class CompanyController : Controller
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
            return View(viewModel);
        }

        [HttpGet]
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
            return View(company);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var viewModel = new CreateCompanyViewModel();
            
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

            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCompanyViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var companyCreateDto = _mapper.Map<CreateCompanyDto>(viewModel);
                var createdCompany = await _companyService.CreateCompanyAsync(companyCreateDto);
                if (createdCompany != null)
                {
                    TempData["SuccessMessage"] = "Company created successfully!";
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "Could not create company.");
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

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyDto = await _companyService.GetCompanyByIdAsync(id.Value);
            if (companyDto == null)
            {
                return NotFound();
            }

            // Map the CompanyDto to CompanyUpdateDto for the view
            var companyUpdateDto = new UpdateCompanyDto
            {
                Id = companyDto.Id,
                Name = companyDto.Name,
                BusinessId = companyDto.BusinessId,
                Address = companyDto.Address,
                City = companyDto.City,
                UserId = companyDto.UserId,
                CreationDate = companyDto.CreationDate,
                // IsDeleted is not explicitly mapped here, handled by service
            };


            return View(companyUpdateDto);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UpdateCompanyDto companyUpdateDto)
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
                    TempData["SuccessMessage"] = "Company updated successfully!";
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "Could not update company. It might not exist or is already deleted.");
            }
            return View(companyUpdateDto);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
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
            return View(company);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var isDeleted = await _companyService.DeleteCompanyAsync(id);
            if (isDeleted)
            {
                TempData["SuccessMessage"] = "Company successfully soft-deleted!";
                return RedirectToAction(nameof(Index));
            }
            TempData["ErrorMessage"] = "Could not delete company. It might not exist or is already deleted.";
            return RedirectToAction(nameof(Index));
        }

    }
}

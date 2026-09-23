using AutoMapper;
using CRM.WebApp.DTOs.Company;
using CRM.WebApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Api.Controllers
{
    /// <summary>
    /// API controller for managing companies
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;
        private readonly ILogger<CompaniesController> _logger;

        public CompaniesController(
            ICompanyService companyService, 
            IMapper mapper,
            ILogger<CompaniesController> logger)
        {
            _companyService = companyService ?? throw new ArgumentNullException(nameof(companyService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Get all companies
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompanyDto>>> GetCompanies()
        {
            try
            {
                var companies = await _companyService.GetAllCompaniesAsync();
                return Ok(companies);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving companies");
                return StatusCode(500, new { message = "Internal server error occurred while retrieving companies" });
            }
        }

        /// <summary>
        /// Get a specific company by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<CompanyDto>> GetCompany(int id)
        {
            try
            {
                var company = await _companyService.GetCompanyByIdAsync(id);
                if (company == null)
                {
                    return NotFound(new { message = $"Company with ID {id} not found" });
                }

                return Ok(company);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving company with ID {CompanyId}", id);
                return StatusCode(500, new { message = "Internal server error occurred while retrieving company" });
            }
        }

        /// <summary>
        /// Create a new company
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<CompanyDto>> CreateCompany([FromBody] CreateCompanyDto createCompanyDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var createdCompany = await _companyService.CreateCompanyAsync(createCompanyDto);
                if (createdCompany == null)
                {
                    return BadRequest(new { message = "Failed to create company" });
                }

                return CreatedAtAction(
                    nameof(GetCompany), 
                    new { id = createdCompany.Id }, 
                    createdCompany);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating company");
                return StatusCode(500, new { message = "Internal server error occurred while creating company" });
            }
        }

        /// <summary>
        /// Update an existing company
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCompany(int id, [FromBody] UpdateCompanyDto updateCompanyDto)
        {
            try
            {
                if (id != updateCompanyDto.Id)
                {
                    return BadRequest(new { message = "ID mismatch" });
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = await _companyService.UpdateCompanyAsync(updateCompanyDto);
                if (!result)
                {
                    return NotFound(new { message = $"Company with ID {id} not found or could not be updated" });
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating company with ID {CompanyId}", id);
                return StatusCode(500, new { message = "Internal server error occurred while updating company" });
            }
        }

        /// <summary>
        /// Delete a company (soft delete)
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            try
            {
                var result = await _companyService.DeleteCompanyAsync(id);
                if (!result)
                {
                    return NotFound(new { message = $"Company with ID {id} not found or could not be deleted" });
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting company with ID {CompanyId}", id);
                return StatusCode(500, new { message = "Internal server error occurred while deleting company" });
            }
        }
    }
}
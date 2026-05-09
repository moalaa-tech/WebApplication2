using AutoMapper;
using CRM.Domain.Entities;
using CRM.WebApp.DTOs.Company;
using CRM.WebApp.Repositories;
using CRM.WebApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CRM.WebApp.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly IRepository<Company> _CompanyRepository;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;
        private readonly ILogger<CompanyService> _logger;
        private const string COMPANIES_CACHE_KEY = "companies:all";
        private const string COMPANY_CACHE_PREFIX = "company:";

        public CompanyService(
            IRepository<Company> companyRepository, 
            IMapper mapper,
            ICacheService cacheService,
            ILogger<CompanyService> logger)
        {
            _CompanyRepository = companyRepository;
            _mapper = mapper;
            _cacheService = cacheService;
            _logger = logger;
        }

        public async Task<IEnumerable<CompanyDto>> GetAllCompaniesAsync()
        {
            return await _cacheService.GetOrCreateAsync(
                COMPANIES_CACHE_KEY,
                async () =>
                {
                    var companies = await _CompanyRepository.GetAll()
                        .Where(c => c.IsDeleted == 0) // Only active companies
                        .OrderBy(c => c.Name)
                        .ToListAsync();
                    
                    var companyDtos = _mapper.Map<IEnumerable<CompanyDto>>(companies);
                    _logger.LogDebug("Loaded {Count} companies from database", companies.Count);
                    return companyDtos;
                },
                TimeSpan.FromMinutes(15) // Companies change frequently
            ) ?? Enumerable.Empty<CompanyDto>();
        }

        public async Task<CompanyDto?> GetCompanyByIdAsync(int id)
        {
            var cacheKey = $"{COMPANY_CACHE_PREFIX}{id}";
            var company = await _CompanyRepository.GetByIdAsync(id);
            
            // Return null if company doesn't exist or is deleted
            if (company == null || company.IsDeleted == 1)
            {
                _cacheService.Remove(cacheKey); // Clear cache if deleted
                return null;
            }
            
            // Use cache for non-deleted companies
            return await _cacheService.GetOrCreateAsync(
                cacheKey,
                async () =>
                {
                    var companyDto = _mapper.Map<CompanyDto>(company);
                    _logger.LogDebug("Loaded company {CompanyId} from database", id);
                    return companyDto;
                },
                TimeSpan.FromMinutes(30)
            );
        }

        public async Task<CompanyDto> CreateCompanyAsync(CreateCompanyDto createCompanyDto)
        {
            var company = _mapper.Map<Company>(createCompanyDto);
            company.CreationDate = DateTime.Now; // Set creation date
            company.IsDeleted = 0; // Default to not deleted
            await _CompanyRepository.AddAsync(company);
            await _CompanyRepository.SaveChangesAsync(); // Persist to database
            
            // Invalidate cache when new company is created
            _cacheService.Remove(COMPANIES_CACHE_KEY);
            _logger.LogInformation("Created new company {CompanyId} and invalidated companies cache", company.Id);
            
            return _mapper.Map<CompanyDto>(company);
        }

        public async Task<bool> UpdateCompanyAsync(UpdateCompanyDto updateCompanyDto)
        {
            // First, fetch the existing company to avoid temporary ID issues
            var existingCompany = await _CompanyRepository.GetByIdAsync(updateCompanyDto.Id);
            if (existingCompany == null)
            {
                _logger.LogWarning("Attempted to update non-existent company {CompanyId}", updateCompanyDto.Id);
                return false;
            }
            
            // Map update DTO to existing entity instead of creating a new one
            _mapper.Map(updateCompanyDto, existingCompany);
            _CompanyRepository.Update(existingCompany);
            await _CompanyRepository.SaveChangesAsync(); // Persist to database
            
            // Invalidate cache when company is updated
            _cacheService.Remove(COMPANIES_CACHE_KEY);
            _cacheService.Remove($"{COMPANY_CACHE_PREFIX}{updateCompanyDto.Id}");
            _logger.LogInformation("Updated company {CompanyId} and invalidated cache", updateCompanyDto.Id);
            
            return true;
        }

        public async Task<bool> DeleteCompanyAsync(int id)
        {
            // For a "soft delete" where IsDeleted is set to 1 instead of actually removing the record:
            var company = await _CompanyRepository.GetByIdAsync(id);
            if (company != null)
            {
                company.IsDeleted = 1; // Set to deleted
                _CompanyRepository.Update(company);
                await _CompanyRepository.SaveChangesAsync(); // Persist to database
                
                // Invalidate cache when company is deleted
                _cacheService.Remove(COMPANIES_CACHE_KEY);
                _cacheService.Remove($"{COMPANY_CACHE_PREFIX}{id}");
                _logger.LogInformation("Deleted company {CompanyId} and invalidated cache", id);
                
                return true;
            }
            // For a hard delete (physically remove from DB):
            // await _companyRepository.DeleteCompanyAsync(id);
            return false;
        }

        public async Task<bool> CompanyExistsAsync(int id)
        {
            var company = await _CompanyRepository.GetByIdAsync(id);
            if (company != null)
                return true;
            return false;
        }

    }
}

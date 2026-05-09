using CRM.WebApp.Services.Interfaces;
using CRM.WebApp.DbContext;
using CRM.WebApp.DTOs.Business;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace CRM.WebApp.Services
{
    public class LookupService : ILookupService
    {
        private readonly ApplicationContext _context;
        private readonly ICacheService _cacheService;
        private readonly IMapper _mapper;
        private readonly ILogger<LookupService> _logger;

        public LookupService(
            ApplicationContext context,
            ICacheService cacheService,
            IMapper mapper,
            ILogger<LookupService> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _cacheService = cacheService ?? throw new ArgumentNullException(nameof(cacheService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IEnumerable<dynamic>> GetCountriesAsync()
        {
            return await _cacheService.GetOrCreateAsync(
                CacheExtensions.GetActiveLookupKey(CacheExtensions.COUNTRIES_KEY),
                async () =>
                {
                    var countries = await _context.Countries
                        .Where(c => c.IsActive)
                        .OrderBy(c => c.Name)
                        .Select(c => new { c.Id, c.Name, c.NameAr, c.ISO2, c.ISO3 })
                        .ToListAsync();
                    
                    _logger.LogDebug("Loaded {Count} countries from database", countries.Count);
                    return countries.Cast<dynamic>();
                },
                TimeSpan.FromHours(2) // Countries change infrequently
            ) ?? Enumerable.Empty<dynamic>();
        }

        public async Task<IEnumerable<dynamic>> GetStatesByCountryAsync(int countryId)
        {
            var cacheKey = CacheExtensions.GetStatesKey(countryId);
            return await _cacheService.GetOrCreateAsync(
                cacheKey,
                async () =>
                {
                    var states = await _context.States
                        .Where(s => s.CountryId == countryId && s.IsActive)
                        .OrderBy(s => s.Name)
                        .Select(s => new { s.Id, s.Name, s.NameAr, s.CountryId })
                        .ToListAsync();
                    
                    _logger.LogDebug("Loaded {Count} states for country {CountryId} from database", states.Count, countryId);
                    return states.Cast<dynamic>();
                },
                TimeSpan.FromHours(1)
            ) ?? Enumerable.Empty<dynamic>();
        }

        public async Task<IEnumerable<dynamic>> GetCitiesByStateAsync(int stateId)
        {
            var cacheKey = CacheExtensions.GetCitiesKey(stateId);
            return await _cacheService.GetOrCreateAsync(
                cacheKey,
                async () =>
                {
                    var cities = await _context.Cities
                        .Where(c => c.StateId == stateId && c.IsActive)
                        .OrderBy(c => c.Name)
                        .Select(c => new { c.Id, c.Name, c.NameAr, c.StateId })
                        .ToListAsync();
                    
                    _logger.LogDebug("Loaded {Count} cities for state {StateId} from database", cities.Count, stateId);
                    return cities.Cast<dynamic>();
                },
                TimeSpan.FromMinutes(30)
            ) ?? Enumerable.Empty<dynamic>();
        }

        public async Task<IEnumerable<BusinessDto>> GetBusinessesAsync()
        {
            return await _cacheService.GetOrCreateAsync(
                CacheExtensions.GetActiveLookupKey(CacheExtensions.BUSINESSES_KEY),
                async () =>
                {
                    var businesses = await _context.Businesses
                        .Where(b => b.IsActive)
                        .OrderBy(b => b.Name)
                        .ToListAsync();
                    
                    var businessDtos = _mapper.Map<IEnumerable<BusinessDto>>(businesses);
                    _logger.LogDebug("Loaded {Count} businesses from database", businesses.Count);
                    return businessDtos;
                },
                TimeSpan.FromMinutes(45)
            ) ?? Enumerable.Empty<BusinessDto>();
        }

        public async Task<IEnumerable<dynamic>> GetCurrenciesAsync()
        {
            return await _cacheService.GetOrCreateAsync(
                CacheExtensions.GetActiveLookupKey(CacheExtensions.CURRENCIES_KEY),
                async () =>
                {
                    var currencies = await _context.Currencies
                        .Where(c => c.IsActive)
                        .OrderBy(c => c.Name)
                        .Select(c => new { c.Id, c.Name, c.Code, c.NameAr })
                        .ToListAsync();
                    
                    _logger.LogDebug("Loaded {Count} currencies from database", currencies.Count);
                    return currencies.Cast<dynamic>();
                },
                TimeSpan.FromHours(1)
            ) ?? Enumerable.Empty<dynamic>();
        }

        public async Task<IEnumerable<dynamic>> GetProductTypesAsync()
        {
            return await _cacheService.GetOrCreateAsync(
                CacheExtensions.GetActiveLookupKey(CacheExtensions.PRODUCT_TYPES_KEY),
                async () =>
                {
                    var productTypes = await _context.ProductTypes
                        .Where(pt => pt.IsActive)
                        .OrderBy(pt => pt.Name)
                        .Select(pt => new { pt.Id, pt.Name, pt.Description })
                        .ToListAsync();
                    
                    _logger.LogDebug("Loaded {Count} product types from database", productTypes.Count);
                    return productTypes.Cast<dynamic>();
                },
                TimeSpan.FromMinutes(30)
            ) ?? Enumerable.Empty<dynamic>();
        }

        public async Task<IEnumerable<dynamic>> GetDepartmentsAsync()
        {
            return await _cacheService.GetOrCreateAsync(
                CacheExtensions.GetActiveLookupKey(CacheExtensions.DEPARTMENTS_KEY),
                async () =>
                {
                    var departments = await _context.Departments
                        .Where(d => d.IsActive)
                        .OrderBy(d => d.Name)
                        .Select(d => new { d.Id, d.Name, d.Description })
                        .ToListAsync();
                    
                    _logger.LogDebug("Loaded {Count} departments from database", departments.Count);
                    return departments.Cast<dynamic>();
                },
                TimeSpan.FromMinutes(20)
            ) ?? Enumerable.Empty<dynamic>();
        }

        public void InvalidateLookupCache(string lookupType)
        {
            var prefix = lookupType.ToLowerInvariant() switch
            {
                "countries" => CacheExtensions.COUNTRIES_KEY,
                "states" => CacheExtensions.STATES_KEY,
                "cities" => CacheExtensions.CITIES_KEY,
                "businesses" => CacheExtensions.BUSINESSES_KEY,
                "currencies" => CacheExtensions.CURRENCIES_KEY,
                "product-types" => CacheExtensions.PRODUCT_TYPES_KEY,
                "departments" => CacheExtensions.DEPARTMENTS_KEY,
                _ => lookupType
            };
            
            _cacheService.InvalidateByPrefix(prefix);
            _logger.LogInformation("Invalidated cache for lookup type: {LookupType}", lookupType);
        }
    }
}

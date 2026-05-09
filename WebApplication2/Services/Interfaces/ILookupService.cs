using CRM.WebApp.DTOs.Company;
using CRM.WebApp.DTOs.Business;
using CRM.WebApp.DTOs.User;

namespace CRM.WebApp.Services.Interfaces
{
    public interface ILookupService
    {
        /// <summary>
        /// Gets all active countries (cached)
        /// </summary>
        Task<IEnumerable<dynamic>> GetCountriesAsync();
        
        /// <summary>
        /// Gets states for a specific country (cached)
        /// </summary>
        Task<IEnumerable<dynamic>> GetStatesByCountryAsync(int countryId);
        
        /// <summary>
        /// Gets cities for a specific state (cached)
        /// </summary>
        Task<IEnumerable<dynamic>> GetCitiesByStateAsync(int stateId);
        
        /// <summary>
        /// Gets all active businesses (cached)
        /// </summary>
        Task<IEnumerable<BusinessDto>> GetBusinessesAsync();
        
        /// <summary>
        /// Gets all active currencies (cached)
        /// </summary>
        Task<IEnumerable<dynamic>> GetCurrenciesAsync();
        
        /// <summary>
        /// Gets all active product types (cached)
        /// </summary>
        Task<IEnumerable<dynamic>> GetProductTypesAsync();
        
        /// <summary>
        /// Gets all active departments (cached)
        /// </summary>
        Task<IEnumerable<dynamic>> GetDepartmentsAsync();
        
        /// <summary>
        /// Invalidates cached lookup data when changes occur
        /// </summary>
        void InvalidateLookupCache(string lookupType);
    }
}

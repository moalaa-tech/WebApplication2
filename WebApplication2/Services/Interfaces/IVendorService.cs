using CRM.WebApp.DTOs.Vendor;

namespace CRM.WebApp.Services.Interfaces
{
    public interface IVendorService
    {
        Task<IEnumerable<VendorDto>> GetAllAsync(string search = null);
        Task<VendorDto> GetByIdAsync(int id);
        Task<bool> AddAsync(VendorDto dto);
        Task<bool> UpdateAsync(VendorDto dto);
        Task<bool> DeleteAsync(int id);
    }
}

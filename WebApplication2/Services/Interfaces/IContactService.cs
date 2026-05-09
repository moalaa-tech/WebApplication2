using CRM.WebApp.DTOs.Contact;

namespace CRM.WebApp.Services.Interfaces
{
    public interface IContactService
    {
        Task<IEnumerable<ContactDto>> GetAllAsync();
        Task<ContactDto> GetByIdAsync(int id);
        Task AddAsync(CreateContactDto dto);
        Task UpdateAsync(UpdateContactDto dto);
        Task DeleteAsync(int id);
    }
}

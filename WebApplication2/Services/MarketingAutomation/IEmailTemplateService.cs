using CRM.WebApp.DTOs.EmailTemplate;

namespace CRM.WebApp.Services.MarketingAutomation
{
    public interface IEmailTemplateService
    {
        Task<IEnumerable<EmailTemplateDto>> GetAllAsync();
        Task<EmailTemplateDto> GetByIdAsync(int id);
        Task CreateAsync(CreateEmailTemplateDto dto);
        Task UpdateAsync(UpdateEmailTemplateDto dto);
        Task DeleteAsync(int id);
    }
}

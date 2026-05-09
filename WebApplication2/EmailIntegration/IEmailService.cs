using CRM.Domain.Entities;
using CRM.Domain.Entities.MarketingAutomation;

namespace CRM.WebApp.EmailIntegration
{
    public interface IEmailService
    {
        Task SendEmailAsync(string to, string subject, string body);
        Task SendTemplateEmail(EmailTemplate template, string email, Contact contact);
    }
}

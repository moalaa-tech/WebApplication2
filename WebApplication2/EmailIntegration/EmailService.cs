using CRM.Domain.Entities;
using CRM.Domain.Entities.MarketingAutomation;
using CRM.WebApp.Settings;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace CRM.WebApp.EmailIntegration
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        private readonly SmtpSettings _smtpSettings;

        public EmailService(IConfiguration config, IOptions<SmtpSettings> smtpOptions)
        {
            _config = config;
            _smtpSettings = smtpOptions.Value;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var client = new SmtpClient(_config["Smtp:Host"])
            {
                Port = int.Parse(_config["Smtp:Port"]),
                Credentials = new NetworkCredential(_config["Smtp:User"], _config["Smtp:Pass"]),
                EnableSsl = true
            };

            var mail = new MailMessage(_config["Smtp:From"], to, subject, body);
            await client.SendMailAsync(mail);
        }

        public async Task SendTemplateEmail(EmailTemplate template, string email, Contact contact)
        {
            if (template == null || string.IsNullOrWhiteSpace(email) || contact == null)
                throw new ArgumentException("Missing template, email or contact");

            // Replace placeholders in subject and content
            string subject = ReplacePlaceholders(template.Subject, contact);
            string body = ReplacePlaceholders(template.Content, contact);

            using var message = new MailMessage
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
                From = new MailAddress(_smtpSettings.FromEmail, _smtpSettings.FromName)
            };

            message.To.Add(email);

            using var client = new SmtpClient(_smtpSettings.Host, _smtpSettings.Port)
            {
                EnableSsl = _smtpSettings.EnableSsl,
                Credentials = new NetworkCredential(_smtpSettings.Username, _smtpSettings.Password)
            };

            await client.SendMailAsync(message);
        }

        private string ReplacePlaceholders(string input, Contact contact)
        {
            if (string.IsNullOrEmpty(input)) return input;

            return input
                .Replace("{{FirstName}}", contact.Name ?? "")
                .Replace("{{LastName}}", contact.Surname ?? "")
                .Replace("{{Email}}", contact.Email ?? "");
        }

    }
}

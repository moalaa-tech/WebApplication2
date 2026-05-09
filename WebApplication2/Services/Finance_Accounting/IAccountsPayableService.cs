using CRM.Domain.Entities.AccountsPayable;
using CRM.WebApp.DTOs.AccountsPayable;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CRM.WebApp.Services.Finance_Accounting
{
    public interface IAccountsPayableService
    {
        Task<IEnumerable<InvoiceDto>> GetAllAsync();
        Task<InvoiceDto> GetByIdAsync(int id);
        Task CreateAsync(InvoiceDto dto);
        Task UpdateAsync(InvoiceDto dto);
        Task DeleteAsync(int id);
        Task<IEnumerable<SelectListItem>> GetVendorsSelectListAsync();
        Task RecordPaymentAsync(Payment payment);
        Task<object> GenerateInvoiceReportAsync(DateTime? startDate, DateTime? endDate);
    }
}
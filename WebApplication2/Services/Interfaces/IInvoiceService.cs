using CRM.WebApp.ViewModels;

namespace CRM.WebApp.Services.Interfaces
{
    public interface IInvoiceService
    {
        IQueryable<InvoiceViewModel> invoices();
    }
}

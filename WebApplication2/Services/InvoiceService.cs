using CRM.Domain.Entities;
using CRM.WebApp.Repositories;
using CRM.WebApp.Services.Interfaces;
using CRM.WebApp.ViewModels;

namespace CRM.WebApp.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IRepository<Order> OrderRepository;
        private readonly IRepository<OrderDetails> OrderDetailsRepositoryRepository;
        public InvoiceService(IRepository<Order> _OrderRepository)
        {
            OrderRepository = _OrderRepository;
        }



        public IQueryable<InvoiceViewModel> invoices()
        {
            throw new NotImplementedException();
        }
    }
}

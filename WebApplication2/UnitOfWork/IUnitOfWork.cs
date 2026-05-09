using CRM.Domain.Base;
using CRM.Domain.Entities.InventoryManagement;
using CRM.Domain.Entities.MarketingAutomation;
using CRM.WebApp.Repositories;

namespace CRM.WebApp.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        public IRepository<Warehouse> Warehouses { get; }
        public IRepository<StockTransaction> StockTransactions { get; }
        public IRepository<ReorderRule> ReorderRules { get; }
        public IRepository<Product> Products { get; set; }
        public IRepository<Batch> Batches { get; set; }
        public IRepository<ProductBarcode> ProductBarcodes { get; set; }

        public IRepository<Campaign> Campaigns { get; set; }
        public IRepository<EmailTemplate> EmailTemplates { get; set; }
        public IRepository<T> Repository<T>() where T : BaseEntity;

        Task<int> CompleteAsync();
    }
}
